using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Bus_Booking_System_DAO
{
    public class Cancellation
    {
        [Key]
        [Required(ErrorMessage = "Cancellation ID is required.")]
        public int CancellationId { get; set; }

        [Required(ErrorMessage = "Booking ID is required.")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "Cancellation Status is required.")]
        public string? CancellationStatus { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        public string? Reason { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking? Booking { get; set; }
    }

}