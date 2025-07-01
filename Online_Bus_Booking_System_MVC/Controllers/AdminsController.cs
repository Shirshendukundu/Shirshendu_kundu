using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class AdminsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Admins/{admin.AdminName}/{admin.Password}");
            if (response.IsSuccessStatusCode)
            {
                TempData["AlertMessage"] = "Login successful! Welcome to Online Bus Ticket Booking.";
                return RedirectToAction("Index", "Home");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                TempData["AlertMessage"] = "Invalid login attempt. Please try again.";
                ModelState.AddModelError("", "Invalid Login Credentials.");
            }

            return View(admin);
        }

        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Default");
            }
            return View(admin);
        }

        public async Task<IActionResult> Default()
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Admins");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var admins = JsonConvert.DeserializeObject<List<Admin>>(jsondata);
                return View(admins);
            }
            return View();
        }

        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(admin);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Admins", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(admin);
        }

        public async Task<IActionResult> EditAdmin(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Admins/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var admin = JsonConvert.DeserializeObject<Admin>(jsondata);
                return View(admin);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(admin);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Admins/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(admin);
        }

        public async Task<IActionResult> RemoveAdmin(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Admins/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var admin = JsonConvert.DeserializeObject<Admin>(jsondata);
                return View(admin);
            }
            return NotFound();
        }

        [HttpPost, ActionName("RemoveAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Admins/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();
        }
    }
}
