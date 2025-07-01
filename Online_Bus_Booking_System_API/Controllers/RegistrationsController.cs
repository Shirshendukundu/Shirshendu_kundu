using Microsoft.AspNetCore.Mvc;
using Online_Bus_Booking_System_DAO;

namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public RegistrationsController(OBTBSDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IEnumerable<Registration> GetRegistrations()
        {
            return _context.Registrations.ToList();
        }

        [HttpGet("{id}")]
        public Registration? GetRegistration(int id)
        {
            var registration = _context.Registrations.FirstOrDefault(m => m.RegistrationId == id);
            return registration;
        }

        [HttpPost]
        public void PostRegistration([FromBody] Registration registration)
        {
            _context.Registrations.Add(registration);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void PutRegistration(int id, [FromBody] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Update(registration);
                _context.SaveChanges();
            }

        }
    }
}