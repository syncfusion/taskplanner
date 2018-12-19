
namespace TaskPlanner.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.ComponentModel.DataAnnotations;

	public class ProjectObjects
    {
		public List<ProjectListObjects> ProjectListObjects { get; set; }

	}

	public class ProjectListObjects
	{
		public int ProjectId { get; set; }

		public string ProjectName { get; set; }

		public string ProjectDescription { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public bool IsOwner { get; set; }

		public string Email { get; set; }
        public bool IsFavourite { get; internal set; }
    }


	public class ProjectShareObjects
	{
		public List<ProjectShareListObjects> ProjectShareListObjects { get; set; }

	}

	public class ProjectShareListObjects
	{
		public int PermissionId { get; set; }

		public string EmailId { get; set; }
	}
}
