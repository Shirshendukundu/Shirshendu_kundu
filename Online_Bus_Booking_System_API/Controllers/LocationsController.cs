using Microsoft.AspNetCore.Mvc;
using Online_Bus_Booking_System_DAO;

namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public LocationsController(OBTBSDbContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations.ToList();
        }

        [HttpGet("{id}")]
        public Location? GetLocation(int id)
        {
            var location = _context.Locations.FirstOrDefault(m => m.LocationId == id);
            return location;
        }

        [HttpPost]
        public void PostLocation([FromBody] Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void PutLocation(int id, [FromBody] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Update(location);
                _context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void RemoveLocation(int id)
        {
            var location = _context.Locations.Find(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
            }

            _context.SaveChanges();
        }
    }
}