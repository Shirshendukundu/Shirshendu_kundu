using Microsoft.EntityFrameworkCore;
using Online_Bus_Booking_System_DAO;

namespace Online_Bus_Booking_System_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<OBTBSDbContext>(options => options.UseSqlServer("Server=DESKTOP-IN7SUG4;Database=BusBookingDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=False"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
