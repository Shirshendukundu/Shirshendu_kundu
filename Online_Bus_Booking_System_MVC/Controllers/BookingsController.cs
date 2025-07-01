using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_Bus_Booking_System_DAO;
using System.Net;
using System.Text;

namespace Online_Bus_Booking_System_MVC.Controllers
{
    public class BookingsController : Controller
    {
        private readonly HttpClient _httpClient;
        public BookingsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
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
        }*/
        /*public IActionResult RegisterUser()
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
            var response = await _httpClient.GetAsync("http://localhost:5131/api/Bookings");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<Booking>>(jsondata);
                if (bookings != null)
                {
                    foreach (var book in bookings)
                    {
                        response = await _httpClient.GetAsync($"http://localhost:5131/api/Buses/{book.BusId}");
                        if (response.IsSuccessStatusCode)
                        {
                            jsondata = await response.Content.ReadAsStringAsync();
                            var bus = JsonConvert.DeserializeObject<Bus>(jsondata);

                            response = await _httpClient.GetAsync($"http://localhost:5131/api/Locations/{book.LocationId}");
                            if (response.IsSuccessStatusCode)
                            {
                                jsondata = await response.Content.ReadAsStringAsync();
                                var location = JsonConvert.DeserializeObject<Location>(jsondata);


                                if (location != null)
                                    book.Location = location;
                                if (bus != null)
                                    book.Bus = bus;                                
                            }
                        }

                    }
                    return View(bookings);
                }
                    
            }
            return View();
        }

        public IActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(booking);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5131/api/Bookings", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(booking);


        }


        public async Task<IActionResult> EditBooking(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Bookings/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<Booking>(jsondata);
                return View(booking);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var jsondata = JsonConvert.SerializeObject(booking);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"http://localhost:5131/api/Bookings/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Default));
                }
            }
            return View(booking);
        }

        public async Task<IActionResult> CancelBooking(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5131/api/Bookings/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<Booking>(jsondata);
                return View(booking);
            }
            return NotFound();
        }

        [HttpPost, ActionName("CancelBooking")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5131/api/Bookings/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Default));
            }
            return View();

        }
    }
}