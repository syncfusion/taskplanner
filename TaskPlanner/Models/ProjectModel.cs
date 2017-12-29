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
													  from b in context.Favourites.Where(y => (y.UserId == c.Id && y.IsActive))
													  from a in context.Projects.Where(x => (x.IsActive && x.ProjectId == b.ProjectId))
													  select new ProjectListObjects
													  {
														  ProjectId = a.ProjectId,
														  ProjectName = a.ProjectName,
														  ProjectDescription = a.Description,
														  CreatedOn = a.CreatedOn,
														  CreatedBy = c.UserName,
														  Email = c.Email
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
														  Email = b.Email
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
														  Email = b.Email
													  }).Distinct().ToList();
				}
			}

			return projectList;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static ProjectShareObjects GetProjectSharedList()
		{
			var list = new ProjectShareObjects();
			using (var context = new TaskPlannerEntities())
			{
				list.ProjectShareListObjects = (from a in context.ProjectPermissions.Where(x => (x.IsActive))
						select new ProjectShareListObjects
						{
							PermissionId = a.PermissionId,
							EmailId = a.EmailId
						}).Distinct().ToList();
			}				
			return list;
		}
	}
}
