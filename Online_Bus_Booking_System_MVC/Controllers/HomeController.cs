using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using Online_Bus_Booking_System_MVC.Models;
using System.Diagnostics;
using System.Net;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public IActionResult User()
        {
            return View();
        }

        public IActionResult Default()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["AlertMessage"] = "You have been logged out.";
            return RedirectToAction("Login", "Users");
        }
    }
}
