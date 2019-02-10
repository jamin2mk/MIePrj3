using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIData.Models
{
    public class ChangePassword
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage ="Please Enter Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name ="New Password")]
        [Required(ErrorMessage = "Please Enter New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [Required(ErrorMessage = "Please Enter Confirm New Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
