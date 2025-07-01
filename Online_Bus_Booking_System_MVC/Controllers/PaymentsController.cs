using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly HttpClient _httpClient;
        public PaymentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /* public IActionResult Login()
         {
             return View();
         }
         [HttpPost]
         public async Task<IActionResult> Login(User user)
         {
             var response = await _httpClient.GetAsync($"http://localhost:5131/api/Users?{user.UserName}/{user.Password}");
             if (response.IsSuccessStatusCode)
             {
                 return RedirectToAction("User", "Home");
             }
             else if (response.StatusCode == HttpStatusCode.NotFound)
             {
                 // Handle successful login logic here
                 return RedirectToAction("Default", "Home");
             }
             else
             {
                 ModelState.AddModelError("", "Invalid login attempt.");
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
         }*/
        public async Task<IActionResult> Default()
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Payments");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var payments = JsonConvert.DeserializeObject<List<Payment>>(jsondata);
                return View(payments);
            }
            return View();
        }
        public IActionResult CreatePayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(payment);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Payments", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(payment);


        }
     
        public async Task<IActionResult> EditPayment(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var payment = JsonConvert.DeserializeObject<Payment>(jsondata);
                return View(payment);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(payment);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Payments/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(payment);
        }

        public async Task<IActionResult> DeletePayment(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var payment = JsonConvert.DeserializeObject<Payment>(jsondata);
                return View(payment);
            }
            return NotFound();
        }

        [HttpPost, ActionName("DeletePayment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Payments/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }

    }
}