using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Bus_Booking_System_DAO
{
    public class Bus
    {
        [Key]
        [Required(ErrorMessage = "Bus ID is required.")]
        public int BusId { get; set; }

        [Required(ErrorMessage = "Bus No is required.")]
        public int BusNo { get; set; }

        [Required(ErrorMessage = "Bus name is required.")]
        public string? BusName { get; set; }
    }

}