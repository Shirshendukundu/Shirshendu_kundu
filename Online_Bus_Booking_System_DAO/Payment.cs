using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Bus_Booking_System_DAO
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        public string? PaymentMethod { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        public DateTime? PaymentDate { get; set; } // Changed to DateTime for better date handling

        [Required(ErrorMessage = "Payment Status is required.")]
        public string? PaymentStatus { get; set; }

        [Required(ErrorMessage = "Transaction ID is required.")]
        public string? TransactionId { get; set; }
    }

}