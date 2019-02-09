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
        [Required(ErrorMessage = "Please enter the recipient's name.")]
        public string Name { get; set; }

        [Display(Name = "Recipient's Tel")]
        [Required(ErrorMessage = "Please enter the recipient's phone.")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        [Display(Name = "Recipient's Address")]
        [Required(ErrorMessage = "Please enter the recipient's address.")]
        public string Address { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Please select the province.")]
        public string Province { get; set; }

        [Display(Name = "Deliver Date")]
        [Required(ErrorMessage = "Please enter the delivery date.")]
        [DataType(DataType.Date)]
        public DateTime Delivery { get; set; }

        public decimal Total { get; set; }
    }
}