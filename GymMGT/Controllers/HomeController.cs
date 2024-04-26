using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GymMGT.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GymMGT.Controllers
{
    // Controller for handling home-related actions
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Controller for handling home-related actions
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method to display the home page
        public IActionResult Index()
        {
            return View();
        }

        // Action method to display the privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Action method to handle errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Return error view along with error details
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
