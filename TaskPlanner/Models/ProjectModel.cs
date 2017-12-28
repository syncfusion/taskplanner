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

			projectList.ProjectListObjects = new List<ProjectListObjects>();

			projectList.ProjectListObjects.Add(new ProjectListObjects
			{
				ProjectId = 1,
				ProjectName = "HR Portal",
				ProjectDescription = "This project is for managing employees",
				CreatedOn = DateTime.Now,
				CreatedBy = "Arul",
				Email = "TestEmail1"
			});
			projectList.ProjectListObjects.Add(new ProjectListObjects
			{
				ProjectId = 2,
				ProjectName = "Recruitment Portal",
				ProjectDescription = "This project is for managing employees",
				CreatedOn = DateTime.Now,
				CreatedBy = "Arul",
				Email = "TestEmail2"
			});

			projectList.ProjectListObjects.Add(new ProjectListObjects
			{
				ProjectId = 3,
				ProjectName = "Direct Trac",
				ProjectDescription = "This project is for managing employees",
				CreatedOn = DateTime.Now,
				CreatedBy = "Arul",
				Email = "TestEmail3"
			});

			projectList.ProjectListObjects.Add(new ProjectListObjects
			{
				ProjectId = 4,
				ProjectName = "Product Management",
				ProjectDescription = "This project is for managing employees",
				CreatedOn = DateTime.Now,
				CreatedBy = "Arul",
				Email = "TestEmail3"
			});

			using (var context = new TaskPlannerEntities())
			{
				if (filter == "")
				{
					//projectList.ProjectListObjects = (from a in context.Projects.Where(x => (x.IsActive))
					//			   from b in context.AspNetUsers.Where(y => (y.Id == a.CreatedBy && y.Email == currentUserEmail))
					//			   select new ProjectListObjects
					//			   {
					//				   ProjectId = a.ProjectId,
					//				   ProjectName = a.ProjectName,
					//				   ProjectDescription = a.Description,
					//				   CreatedOn = a.CreatedOn,
					//				   CreatedBy = b.UserName,
					//				   Email = b.Email
					//			   }).ToList();
				}
			}

			return projectList;
		}
    }
}
