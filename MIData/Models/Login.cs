using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MIData.Models
{
    public class Login
    {
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
