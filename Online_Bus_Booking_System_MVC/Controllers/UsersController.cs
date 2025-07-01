using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Users/{user.UserName}/{user.Password}");
            if (response.IsSuccessStatusCode)
            {
                TempData["AlertMessage"] = "Login successful! Welcome to Online Bus Ticket Booking.";
                return RedirectToAction("User", "Home");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                TempData["AlertMessage"] = "Invalid login attempt. Please try again.";
                ModelState.AddModelError("", "Invalid Login Credentials.");
            }

            return View(user);
        }
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                // Save user to the database
                return RedirectToAction("Default");
            }
            return View(user);
        }
        public async Task<IActionResult> UserList()
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Users/{TempData["user"]}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<User>(jsondata);
                return View(users);
            }
            return View();
        }

        public async Task<IActionResult> Default()
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Users");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(jsondata);
                return View(users);
            }
            return View();
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Users", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(user);


        }

        public async Task<IActionResult> EditUser(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(jsondata);
                return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Users/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(user);
        }

        public async Task<IActionResult> RemoveUser(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(jsondata);
                return View(user);
            }
            return NotFound();
        }

        [HttpPost, ActionName("RemoveUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }
    }
}
