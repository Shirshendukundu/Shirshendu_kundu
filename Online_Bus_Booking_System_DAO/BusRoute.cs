using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Bus_Booking_System_DAO
{
    public class BusRoute
    {
        [Key]
        public int BusRouteId { get; set; }

        [Required(ErrorMessage = "Bus ID is required.")]
        public int BusId { get; set; }

        public string? BusName { get; set; }

        public int? Distance { get; set; }

        [ForeignKey("BusId")]
        public virtual Bus? Bus { get; set; }
    }

}
