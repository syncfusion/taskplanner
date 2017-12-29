using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskPlanner.Models;
using Microsoft.AspNetCore.Identity; 
using System.Security.Claims;

namespace TaskPlanner.Controllers
{
    public class ProjectController : Controller
    {
		public IActionResult Projects() 
        {
            ViewData["Message"] = "Projects";


            var currentUserEmail = User.Identity.Name;

            var projectList = ProjectModel.GetProjectList("", currentUserEmail);
			return this.View("~/Views/Project/Projects.cshtml", projectList);
		}


        [HttpPost("/project/delete/{projectId}", Name = "Project_Delete")] 
        public JsonResult DeleteProject(int projectId)
        {
            var currentUserEmail = User.Identity.Name;
            bool isOwner = Permission.IsUserOwnerOfProject(currentUserEmail, projectId);

            if(!isOwner)
                return this.Json(new { isSuccess = false,message="Permission Denied" });

            var res = new ProjectViewModel().DeleteProject(projectId);

            if(res!=null && res.IsSuccess)
                return this.Json(new { isSuccess = true, message = "Project deleted successfully" });

            else
                return this.Json(new { isSuccess = false, message = "Unexpected error occurred" }); 
        }

        [HttpPost("/project/favourite/", Name = "Project_Favourite")]
        public JsonResult UpdateFavourite(int projectId,bool isFavourite)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = new ProjectViewModel().UpdateFavouriteList(projectId, userId, isFavourite);

            if (res != null && res.IsSuccess)
            {
                if (isFavourite)
                    return this.Json(new { isSuccess = true, message = "Added to favourite" });
                else
                    return this.Json(new { isSuccess = true, message = "Removed from favourite" });
            }

            else
                return this.Json(new { isSuccess = false, message = "Unexpected error occurred" });
        }

    }
}