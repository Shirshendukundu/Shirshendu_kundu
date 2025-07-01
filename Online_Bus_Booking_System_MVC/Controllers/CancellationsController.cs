using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class CancellationsController : Controller
    {
        private readonly HttpClient _httpClient;
        public CancellationsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Default()
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Cancellations");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var cancellations = JsonConvert.DeserializeObject<List<Cancellation>>(jsondata);
                return View(cancellations);
            }
            return View();
        }

        public IActionResult CreateCancellation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCancellation(Cancellation cancellation)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(cancellation);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Cancellations", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(cancellation);


        }

        public async Task<IActionResult> EditCancellation(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Cancellations/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var cancellation = JsonConvert.DeserializeObject<Cancellation>(jsondata);
                return View(cancellation);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCancellation(int id, Cancellation cancellation)
        {
            if (id != cancellation.CancellationId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(cancellation);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Cancellations/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(cancellation);
        }

        public async Task<IActionResult> RemoveCancellation(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Cancellations/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var cancellation = JsonConvert.DeserializeObject<Cancellation>(jsondata);
                return View(cancellation);
            }
            return NotFound();
        }

        [HttpPost, ActionName("RemoveCancellation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Cancellations/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }
    }
}