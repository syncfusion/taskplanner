namespace TaskPlanner.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
    using TaskPlanner.Entity;

	public class ProjectModel
    {
		public static ProjectObjects GetProjectList(string filter, string currentUserEmail)
		{
			var projectList = new ProjectObjects();

			using (var context = new TaskPlannerEntities())
			{
				if (filter == "favourites")
				{
                    projectList.ProjectListObjects = (from c in context.AspNetUsers.Where(y => (y.Email == currentUserEmail))
                                                      from d in context.ProjectPermissions.Where(i => i.IsActive && i.EmailId == currentUserEmail).DefaultIfEmpty()
                                                      from a in context.Projects.Where(x => (x.IsActive && (x.ProjectId == d.ProjectId || x.CreatedBy == c.Id)))
                                                      from b in context.Favourites.Where(y => (y.UserId == c.Id && y.ProjectId == a.ProjectId && y.IsActive))
                                                      select new ProjectListObjects
                                                      {
                                                          ProjectId = a.ProjectId,
                                                          ProjectName = a.ProjectName,
                                                          ProjectDescription = a.Description,
                                                          CreatedOn = a.CreatedOn,
                                                          CreatedBy = c.UserName,
                                                          Email = c.Email,
                                                          IsOwner = Permission.IsUserOwnerOfProject(currentUserEmail, a.ProjectId)
                                                      }).Distinct().ToList();
				}

				else if (filter == "all")
				{
					projectList.ProjectListObjects = (from b in context.AspNetUsers.Where(i => i.Email == currentUserEmail)
													  from c in context.ProjectPermissions.Where(z => (z.EmailId == currentUserEmail && z.IsActive)).DefaultIfEmpty()
													  from a in context.Projects.Where(x => x.IsActive && (x.ProjectId == c.ProjectId || x.CreatedBy == b.Id))
													  select new ProjectListObjects
													  {
														  ProjectId = a.ProjectId,
														  ProjectName = a.ProjectName,
														  ProjectDescription = a.Description,
														  CreatedOn = a.CreatedOn,
														  CreatedBy = b.UserName,
														  Email = b.Email,
                                                          IsOwner = Permission.IsUserOwnerOfProject(currentUserEmail, a.ProjectId)
                                                      }).Distinct().ToList();
				}
				else
				{
					projectList.ProjectListObjects = (from b in context.AspNetUsers.Where(y => y.Email == currentUserEmail)
													  from a in context.Projects.Where(x => (x.IsActive && x.CreatedBy == b.Id))

													  select new ProjectListObjects
													  {
														  ProjectId = a.ProjectId,
														  ProjectName = a.ProjectName,
														  ProjectDescription = a.Description,
														  CreatedOn = a.CreatedOn,
														  CreatedBy = b.UserName,
														  Email = b.Email,
                                                          IsOwner = Permission.IsUserOwnerOfProject(currentUserEmail, a.ProjectId)
                                                      }).Distinct().ToList();
				}


                if (projectList != null && projectList.ProjectListObjects != null && projectList.ProjectListObjects.Count > 0)
                {
                    var projects = projectList.ProjectListObjects.Select(x => x.ProjectId).ToList();

                    var userid = context.AspNetUsers.Where(x => x.Email == currentUserEmail).Select(x => x.Id).FirstOrDefault();


                    var favouriteObj = (from favouritesDetails in context.Favourites.Where(i => projects.Contains(i.ProjectId) && i.IsActive && i.UserId == userid)
                                        select favouritesDetails.ProjectId).ToList();


                    if (favouriteObj != null)
                    {
                        foreach (var item in projectList.ProjectListObjects)
                        {
                            if (favouriteObj.Contains(item.ProjectId))
                                item.IsFavourite = true;
                        }
                    }
                }
                   


			}

			return projectList;
		}

        public static ProjectObjects GetProjectDetails(int projectId)
        {
            var projectList = new ProjectObjects();

            using (var context = new TaskPlannerEntities())
            {
                    projectList.ProjectListObjects = (from a in context.Projects.Where(x => (x.IsActive && x.ProjectId == projectId))
                                                      from b in context.AspNetUsers.Where(y => y.Id == a.CreatedBy)
                                                      select new ProjectListObjects
                                                      {
                                                          ProjectId = a.ProjectId,
                                                          ProjectName = a.ProjectName,
                                                          ProjectDescription = a.Description,
                                                          CreatedOn = a.CreatedOn,
                                                          CreatedBy = b.UserName,
                                                          Email = b.Email
                                                      }).Distinct().ToList();
                
            }

            return projectList;
        }
    }
}
