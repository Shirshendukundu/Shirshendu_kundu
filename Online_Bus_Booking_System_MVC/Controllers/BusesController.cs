using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class BusesController : Controller
    {
        private readonly HttpClient _httpClient;
        public BusesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /*public IActionResult RegisterBus()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterBus(Location location)
        {
            if (ModelState.IsValid)
            {
                // Save user to the database
                return RedirectToAction("Default");
            }
            return View(location);
        }*/
        public async Task<IActionResult> Default()
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Buses");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var bus = JsonConvert.DeserializeObject<List<Bus>>(jsondata);
                return View(bus);
            }
            return View();
        }        

        public IActionResult CreateBus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBus(Bus bus)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(bus);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Buses", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(bus);


        }

        public async Task<IActionResult> EditBus(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Buses/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var bus = JsonConvert.DeserializeObject<Bus>(jsondata);
                return View(bus);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBus(int id, Bus bus)
        {
            if (id != bus.BusId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(bus);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Buses/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(bus);
        }

        public async Task<IActionResult> RemoveBus(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Buses/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var bus = JsonConvert.DeserializeObject<Bus>(jsondata);
                return View(bus);
            }
            return NotFound();
        }

        [HttpPost, ActionName("RemoveBus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Buses/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }
    }
}