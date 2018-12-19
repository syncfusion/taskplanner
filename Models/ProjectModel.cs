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
                                                      from a in context.Projects.Where(x => (x.IsActive && (x.ProjectId == d.ProjectId)))
                                                      from b in context.Favourites.Where(y => (y.UserId == c.Id && y.ProjectId == a.ProjectId && y.IsActive))
                                                      from owner in context.AspNetUsers.Where(y => (y.Id == a.CreatedBy)
                                                      )
                                                      select new ProjectListObjects
                                                      {
                                                          ProjectId = a.ProjectId,
                                                          ProjectName = a.ProjectName,
                                                          ProjectDescription = a.Description,
                                                          CreatedOn = a.CreatedOn,
                                                          CreatedBy = owner.Email,
                                                          Email = owner.Email,
                                                          IsOwner = owner.Email==currentUserEmail
                                                      }).Distinct().ToList();
				}

				else if (filter == "all")
				{
                    var projPermission = (from c in context.ProjectPermissions where c.EmailId == currentUserEmail && c.IsActive select c.ProjectId).ToList();

                    if(projPermission!=null && projPermission.Count>0)
                      projectList.ProjectListObjects = (
													  from a in context.Projects.Where(x => x.IsActive && projPermission.Contains(x.ProjectId ))
                                                      from b in context.AspNetUsers.Where(i => i.Id == a.CreatedBy)
                                                      
													  select new ProjectListObjects
													  {
														  ProjectId = a.ProjectId,
														  ProjectName = a.ProjectName,
														  ProjectDescription = a.Description,
														  CreatedOn = a.CreatedOn,
														  CreatedBy = b.Email,
														  Email = b.Email,
                                                          IsOwner = b.Email == currentUserEmail
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
														  CreatedBy = b.Email,
														  Email = b.Email,
                                                          IsOwner = true
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

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static ProjectShareObjects GetProjectSharedList(int projectId,string emailid)
		{
			var list = new ProjectShareObjects();
			using (var context = new TaskPlannerEntities())
			{
				list.ProjectShareListObjects = (from a in context.ProjectPermissions.Where(x => (x.IsActive) && x.ProjectId== projectId 
                                                && x.EmailId!=emailid
                                                )  
						select new ProjectShareListObjects
						{
							PermissionId = a.PermissionId,
							EmailId = a.EmailId
						}).Distinct().OrderBy(x => x.EmailId).ToList();
			}				
			return list;
		}
	}
}
