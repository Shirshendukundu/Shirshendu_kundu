using Microsoft.AspNetCore.Mvc;
using Online_Bus_Booking_System_DAO;

namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public BusesController(OBTBSDbContext context)
        {
            _context = context;
        }
      
        [HttpGet]
        public IEnumerable<Bus> GetBuses()
        {
            return _context.Buses.ToList();
        }

        [HttpGet("{id}")]
        public Bus? GetBus(int id)
        {
            var bus = _context.Buses.FirstOrDefault(m => m.BusId == id);
            return bus;
        }

        [HttpPost]
        public void PostBus([FromBody] Bus bus)
        {
            _context.Buses.Add(bus);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void PutBus(int id, [FromBody] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _context.Update(bus);
                _context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void DeleteBus(int id)
        {
            var bus = _context.Buses.Find(id);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
            }

            _context.SaveChanges();
        }
    }
}