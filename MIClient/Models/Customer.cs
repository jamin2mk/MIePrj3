using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MIClient.Models
{
    
    public class Customer
    {
        
        [Key]
        public int cus_id { get; set; }

        
        [Required(ErrorMessage = "First Name is required!")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name maximum 50 characters and minimum 3 characters!")]
        public string cus_fname { get; set; }

        
        [Required(ErrorMessage = "Last Name is required!")]
        [Display(Name = "Last name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name maximum 50 characters and minimum 3 characters!")]
        public string cus_lname { get; set; }

        
        [Display(Name = "Gender")]
        public Nullable<bool> cus_gender { get; set; }


        
        [DataType(DataType.Date, ErrorMessage = "Wrong format date of birth")]
        [Display(Name = "Day of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> cus_dob { get; set; }

        
        [Required(ErrorMessage = "Phone Number is required!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = ("Wrong format phone number"))]
        [Display(Name = "Phone Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone number maximum 12 number and minimum 10 number!")]
        public string cus_phone { get; set; }

        
        [Display(Name = "Address")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Address maximum 50 characters and minimum 3 characters!")]
        public string cus_add { get; set; }


       
        [Display(Name = "Credit Card")]
        [DataType(DataType.CreditCard, ErrorMessage = "Wrong format credit card")]
        [StringLength(23, MinimumLength = 12, ErrorMessage = "Credit card maximum 23 characters and minimum 12 characters!")]
        public string cus_card { get; set; }


       
        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "Email")]
        [StringLength(255, ErrorMessage = "Email address maximum 255 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wrong format email address.")]
        [EmailAddress(ErrorMessage = ("Wrong format email address!"))]
        public string cus_email { get; set; }


        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password maximum 50 characters and minimum 3 characters!")]
        [DataType(DataType.Password, ErrorMessage = "Wrong format password!")]
        public string cus_pass { get; set; }
    }
    

    public class Changepassword
    {
        //tao moit claSss rang buoc thi moi xai editor for dc

        [DataType(DataType.Password)]
        public string Oldpass { get; set; }

        [DataType(DataType.Password)]
        public string pass1 { get; set; }

        [DataType(DataType.Password)]
        public string pass2 { get; set; }
    }
}