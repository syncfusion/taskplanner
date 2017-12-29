using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskPlanner.Base.Stories;
using TaskPlanner.Models;
using TaskPlanner.Objects;
using Newtonsoft.Json;

namespace TaskPlanner.Controllers
{
    public class StoriesController : Controller
    {
        /// <summary>
        /// object for IStoryBaseModel class.
        /// </summary>
        private readonly IStoryBaseModel iStoryBaseModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoriesController"/> class.
        /// </summary>
        /// <param name="room">room is the reference name for IStoryBaseModel</param>
        public StoriesController(IStoryBaseModel story)
        {
            this.iStoryBaseModel = story;
        }

        /// <summary>
        /// Rerunrs stroies page
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>stories view</returns>
        public IActionResult Stories(int projectId)
        {
            ViewBag.ProjectId = projectId;
            var currentUserEmail = User.Identity.Name;

            var hasPermission=Permission.IsUserHasAccessToProject(currentUserEmail,projectId);
           
            

            if (!hasPermission)
                return Content("You dont have permission to access this project. Request owner of project for access permission.");

            StoryModel story = new StoryModel(this.iStoryBaseModel);
            var list = story.GetProjectDetails(projectId); 
            
            ViewBag.ProjectName = list.ProjectName;
            ViewBag.ProjectDescription = list.Description;
            ViewData["Message"] = "Stories";

            return View();
        }

        /// <summary>
        /// Story list page
        /// </summary>
        /// <param name="dataManager">contains the data manger value</param>
        /// <param name="projectId">Project Id</param>
        /// <returns>return the grid list</returns>
        public JsonResult StoriesList([FromBody]DataManager dataManager, int projectId)
        {
            StoryModel story = new StoryModel(this.iStoryBaseModel);
            var list = story.GetStoriesList(dataManager, projectId);
            var settings = new JsonSerializerSettings();
            return this.Json(new { result = list, count = story.TotalListCount }, settings);
        }
    }
}