using Microsoft.EntityFrameworkCore;

namespace Online_Bus_Booking_System_DAO
{
    public class OBTBSDbContext : DbContext
    {
        public OBTBSDbContext(DbContextOptions<OBTBSDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public static bool LoginSuccess { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<Reschedule> Reschedules { get; set; }
        public DbSet<Payment>Payments { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}