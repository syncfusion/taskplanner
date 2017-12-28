using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaskPlanner.Controllers
{
    public class ProjectController : Controller
    { 

        public IActionResult Projects() 
        {
            ViewData["Message"] = "Projects";

            return View();
        }
    }
}