using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIData.Models
{
    public class Recipient
    {
        [Display(Name ="Recipient's Name")]
        public string Name { get; set; }

        [Display(Name = "Recipient's Tel")]
        public string Phone { get; set; }

        [Display(Name = "Recipient's Address")]
        public string Address { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Deliver Date")]
        [DataType(DataType.Date)]
        public DateTime Delivery { get; set; }

        public decimal Total { get; set; }
    }
}