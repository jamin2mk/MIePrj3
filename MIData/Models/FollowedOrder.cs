using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIData.Models
{
    public class FollowedOrder
    {
        [Display(Name = "Date of Order")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Date of Shipping")]
        [DataType(DataType.Date)]
        public DateTime ShipOrder { get; set; }

        public string Payment { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShipAddress { get; set; }

        public string Status { get; set; }
    }
}
