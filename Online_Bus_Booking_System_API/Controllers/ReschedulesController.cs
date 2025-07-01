using Microsoft.AspNetCore.Mvc;
using Online_Bus_Booking_System_DAO;
namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReschedulesController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public ReschedulesController(OBTBSDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Reschedule> GetReschedules()
        {
            return _context.Reschedules.ToList();
        }

        [HttpGet("{id}")]
        public Reschedule? GetReschedule(int id)
        {
            var reschedule = _context.Reschedules.FirstOrDefault(m => m.RescheduleId == id);
            return reschedule;
        }

        [HttpPost]
        public void PostReschedule([FromBody] Reschedule reschedule)
        {
            _context.Reschedules.Add(reschedule);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void PutReschedule(int id, [FromBody] Reschedule reschedule)
        {
            if (ModelState.IsValid)
            {
                _context.Update(reschedule);
                _context.SaveChanges();
            }
        }

        // DELETE api/<AdminsController>/5
        [HttpDelete("{id}")]
        public void DeleteReschedule(int id)
        {
            var reschedule = _context.Reschedules.Find(id);
            if (reschedule != null)
            {
                _context.Reschedules.Remove(reschedule);
            }

            _context.SaveChanges();
        }
    }
}
