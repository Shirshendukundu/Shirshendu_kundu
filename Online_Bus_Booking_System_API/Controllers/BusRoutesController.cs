using Microsoft.AspNetCore.Mvc;
using Online_Bus_Booking_System_DAO;

namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusRoutesController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public BusRoutesController(OBTBSDbContext context)
        {
            _context = context;
        }
  
        [HttpGet]
        public IEnumerable<BusRoute> GetRoutes()
        {
            return _context.BusRoutes.ToList();
        }

        [HttpGet("{id}")]
        public BusRoute? GetBusRoute(int id)
        {
            var busroute = _context.BusRoutes.FirstOrDefault(m => m.BusRouteId == id);
            return busroute;
        }

        [HttpPost]
        public void PostBusRoute([FromBody] BusRoute busroute)
        {
            _context.BusRoutes.Add(busroute);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void PutBusRoute(int id, [FromBody] BusRoute busroute)
        {
            if (ModelState.IsValid)
            {
                _context.Update(busroute);
                _context.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void DeleteBusRoute(int id)
        {
            var busroute = _context.BusRoutes.Find(id);
            if (busroute != null)
            {
                _context.BusRoutes.Remove(busroute);
            }

            _context.SaveChanges();
        }
    }
}