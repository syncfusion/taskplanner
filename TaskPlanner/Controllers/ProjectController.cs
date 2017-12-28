using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskPlanner.Models;
using Microsoft.AspNetCore.Identity;

namespace TaskPlanner.Controllers
{
    public class ProjectController : Controller
    {
		public IActionResult Projects() 
        {
            ViewData["Message"] = "Projects";

			var currentUserEmail = "kailash2289@gmail.com";
			var projectList = ProjectModel.GetProjectList("", currentUserEmail);
			return this.View("~/Views/Project/Projects.cshtml", projectList);
		}


    }
}