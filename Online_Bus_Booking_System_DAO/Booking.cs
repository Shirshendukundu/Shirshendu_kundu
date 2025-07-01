using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Bus_Booking_System_DAO
{
    public class Booking
    {
        [Key]
        [Required(ErrorMessage = "Booking Id is required.")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "RefNo is required.")]
        public int RefNo { get; set; }

        [Required(ErrorMessage = "Passenger name is required.")]
        public string? PassengerName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }

        public virtual Location? Location { get; set; }
        public virtual Bus? Bus { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }

        [ForeignKey("Bus")]
        public int BusId { get; set; }
    }

}