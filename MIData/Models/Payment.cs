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
        [Required(ErrorMessage = "Please select the Payment Method.")]
        public string Mode { get; set; }

        [Display(Name = "Card Owner Name")]
        //[Required(ErrorMessage = "Please select the Card Owner Name.")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        //[Required(ErrorMessage = "Please select the Card Number.")]
        [StringLength(20, MinimumLength = 18)]
        public string CardNumber { get; set; }

        //[Required(ErrorMessage = "Please select the CVC.")]
        [Range(100,999,ErrorMessage = "CVC must just have 3 digits.")]
        public int CVC { get; set; }

        [Display(Name = "Expired Date")]
        //[Required(ErrorMessage = "Please select the Expired Date.")]
        [DataType(DataType.Date)]
        public DateTime ExpiredDate { get; set; }
    }

    public enum PayMode
    {
        Direct,
        CreditCard
    }
}