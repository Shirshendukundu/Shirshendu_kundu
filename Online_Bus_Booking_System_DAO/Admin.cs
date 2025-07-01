using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Bus_Booking_System_DAO
{
    public class Admin
    {
        [Required(ErrorMessage = "AdminID is required.")]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "AdminName is required.")]
        public string? AdminName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? Phone { get; set; }
    }
}
