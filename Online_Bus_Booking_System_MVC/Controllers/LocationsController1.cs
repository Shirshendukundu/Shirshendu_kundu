using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class LocationsController : Controller
    {
        private readonly HttpClient _httpClient;
        public LocationsController(HttpClient httpClient)
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
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Locations");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<List<Location>>(jsondata);
                return View(location);
            }
            return View();
        }

        public IActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLocation(Location location)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(location);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Locations", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(location);


        }

        public async Task<IActionResult> EditLocation(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Locations/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<Location>(jsondata);
                return View(location);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLocation(int id, Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(location);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Locations/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(location);
        }

        public async Task<IActionResult> DeleteLocation(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Locations/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<Location>(jsondata);
                return View(location);
            }
            return NotFound();
        }

        [HttpPost, ActionName("DeleteLocation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Locations/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }
    }
}