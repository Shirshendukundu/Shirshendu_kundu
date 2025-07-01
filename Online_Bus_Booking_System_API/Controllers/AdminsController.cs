using Microsoft.AspNetCore.Mvc;
using Online_Bus_Booking_System_DAO;


namespace Online_Bus_Booking_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly OBTBSDbContext _context;

        public AdminsController(OBTBSDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IEnumerable<Admin> GetAdmins()
        {
            return _context.Admins.ToList();
        }

        
        public Admin? GetAdmin(int id)
        {
            var admin = _context.Admins.FirstOrDefault(m => m.AdminId == id);
            return admin;
        }
        [HttpGet("{username}/{password}")]
        public ActionResult<Admin> GetAdmin(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(m => (m.AdminName == username && m.Password == password));
            if (admin == null)
            {
                return NotFound();
            }
            return admin;
        }
       
        [HttpPost]
        public void PostAdmin([FromBody] Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        [HttpPut("{id}")]
        public void PutAdmin(int id, [FromBody] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Update(admin);
                _context.SaveChanges();
            }
        }

        [HttpDelete("{AdminId}")]
        public void DeleteAdmin(int AdminId)
        {
            var admin = _context.Admins.Find(AdminId);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }
    }
}