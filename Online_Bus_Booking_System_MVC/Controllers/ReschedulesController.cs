using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;
 
namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class ReschedulesController : Controller
    {
        private readonly HttpClient _httpClient;
        public ReschedulesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    
        public async Task<IActionResult> Default()
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Reschedules");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var reschedule = JsonConvert.DeserializeObject<List<Reschedule>>(jsondata);
                return View(reschedule);
            }
            return View();
        }

        public IActionResult CreateReschedule()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReschedule(Reschedule reschedule)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(reschedule);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Reschedules", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(reschedule);


        }

        public async Task<IActionResult> EditReschedule(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Reschedules/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var reschedule = JsonConvert.DeserializeObject<Reschedule>(jsondata);
                return View(reschedule);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReschedule(int id, Reschedule reschedule)
        {
            if (id != reschedule.RescheduleId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(reschedule);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Reschedules/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(reschedule);
        }

        public async Task<IActionResult> RemoveReschedule(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Reschedules/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var reschedule = JsonConvert.DeserializeObject<Reschedule>(jsondata);
                return View(reschedule);
            }
            return NotFound();
        }

        [HttpPost, ActionName("RemoveReschedule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Reschedules/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }
    }
}