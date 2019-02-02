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


  
    public class Size
    {
        [Key]
        public int pr_id { get; set; }
        [Display(Name = "Print Size")]
        [StringLength(20,ErrorMessage ="Print Size must be within 20 characters")]
        [Required(ErrorMessage ="Print Size cannot be blank")]
        public string pr_size { get; set; }
        [Display(Name ="Price")]
        [Required(ErrorMessage ="Price cannot be blank")]
        [Range(1,100000,ErrorMessage ="Price must be within [0-100000]")]
        public decimal pr_price { get; set; }
    }
    [MetadataType(typeof(Size))]
    public partial class tb_printsize { }




}