using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIData.Models
{
    public class Summary
    {
        [Display(Name = "Order Number")]
        public int OrderID { get; set; }

        [Display(Name = "Recipient's Name")]
        public string Name { get; set; }

        [Display(Name = "Recipient's Tel")]
        public string Phone { get; set; }

        [Display(Name = "Recipient's Address")]
        public string Address { get; set; }

        [Display(Name = "Deliver Date")]
        [DataType(DataType.Date)]
        public DateTime Delivery { get; set; }

        [Display(Name = "Total Paid")]
        public decimal Total { get; set; }

        [Display(Name = "Payment Method")]
        public string Mode { get; set; }
    }
}