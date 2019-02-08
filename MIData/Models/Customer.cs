using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIData.Models
{
    public class Customer
    {
        [Key]
        public int cus_id { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        [Display(Name = "First Name")]
        public string cus_fname { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        public string cus_lname { get; set; }

        [Display(Name = "Gender")]
        public bool cus_gender { get; set; }

        [Display(Name = "Day of Birth")]
        [DataType(DataType.Date)]
        public DateTime cus_dob { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Display(Name = "Phone Number")]
        public string cus_phone { get; set; }

        [Required(ErrorMessage = "Please enter your address.")]
        [Display(Name = "Address")]
        public string cus_add { get; set; }

        [Required(ErrorMessage = "Please enter your Credit Card.")]
        [Display(Name = "Credit Card")]
        public string cus_card { get; set; }

        [Required(ErrorMessage = "Please enter your Email.")]
        [Display(Name = "Email")]
        public string cus_email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string cus_pass { get; set; }

        [Required(ErrorMessage = "Please enter the password confirm")]
        [Display(Name = "Password Confirm")]
        [DataType(DataType.Password)]
        public string pass_confirm { get; set; }
    }
}
