using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Bus_Booking_System_DAO
{
    public class Reschedule
    {
        [Key]
        [Required(ErrorMessage = "RescheduleId is required.")]
        public int RescheduleId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "BusName is required.")]
        public string? BusName { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Departure is required.")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "ETA is required.")]
        public DateTime ETA { get; set; }

        [Required(ErrorMessage = "Availability is required.")]
        public int Availability { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
    }

}