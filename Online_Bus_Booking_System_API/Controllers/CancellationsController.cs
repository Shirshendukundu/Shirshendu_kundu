using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bus_Booking_System_DAO;
namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancellationsController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public CancellationsController(OBTBSDbContext context)
        {
            _context = context;
        }
   
        [HttpGet]
        public IEnumerable<Cancellation> GetCancellations()
        {
            return _context.Cancellations.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Cancellation> GetCancellation(int id)
        {
            var cancellation = _context.Cancellations.FirstOrDefault(m => m.CancellationId == id);
            if (cancellation == null)
            {
                return NotFound();
            }
            return cancellation;
        }

        [HttpPost]
        public ActionResult<Cancellation> PostCancellation([FromBody] Cancellation cancellation)
        {
            _context.Cancellations.Add(cancellation);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCancellation), new { id = cancellation.CancellationId }, cancellation);
        }

        [HttpPut("{id}")]
        public IActionResult PutCancellation(int id, [FromBody] Cancellation cancellation)
        {
            if (id != cancellation.CancellationId)
            {
                return BadRequest();
            }

            _context.Entry(cancellation).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCancellation(int id)
        {
            var cancellation = _context.Cancellations.Find(id);
            if (cancellation == null)
            {
                return NotFound();
            }

            _context.Cancellations.Remove(cancellation);
            _context.SaveChanges();

            return NoContent();
        }
    }
}