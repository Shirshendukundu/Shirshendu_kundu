using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bus_Booking_System_DAO;
namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public BookingsController(OBTBSDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IEnumerable<Booking> GetBookings()
        {
            return _context.Bookings.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookings(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        [HttpPost]
        public ActionResult<Booking> PostBooking([FromBody] Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookings), new { id = booking.BookingId }, booking);
        }

        [HttpPut("{id}")]
        public IActionResult PutBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return NoContent();
        }
    }
}