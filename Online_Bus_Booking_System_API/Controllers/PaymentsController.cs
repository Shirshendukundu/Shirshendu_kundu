using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bus_Booking_System_DAO;
namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public PaymentsController(OBTBSDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Payment> GetPayments()
        {
            return _context.Payments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _context.Payments.FirstOrDefault(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpPost]
        public ActionResult<Payment> PostPayment([FromBody] Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public IActionResult PutPayment(int id, [FromBody] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{PaymentId}")]
        public void DeletePayment(int PaymentId)
        {
            var payment = _context.Payments.Find(PaymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}