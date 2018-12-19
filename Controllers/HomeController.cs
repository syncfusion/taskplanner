using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPlanner.Models;

namespace TaskPlanner.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(ProjectController.Projects), "Project");
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
