using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIClient.Models
{
    public class Admin
    {

        [Key]
        public int aid { get; set; }

        [Display(Name = "UserName")]
        public string uid { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string pwd { get; set; }
    }
}