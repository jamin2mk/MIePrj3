using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIData.Models
{
    public class Payment
    {
        [Display(Name = "Payment Method")]
        public string Mode { get; set; }

        [Display(Name = "Card Owner Name")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        public int CVC { get; set; }

        [Display(Name = "Expired Date")]
        [DataType(DataType.Date)]
        public DateTime ExpiredDate { get; set; }
    }

    public enum PayMode
    {
        Direct,
        CreditCard
    }
}