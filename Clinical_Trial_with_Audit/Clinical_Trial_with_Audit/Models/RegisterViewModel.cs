using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Clinical_Trial.Models
{
    public class RegisterViewModel
    {
        [Display(Name ="Register Number")]
        public int Reg_No { get; set; }

        [Display(Name = "Trial Number")]
        [Required(ErrorMessage = "Trial Number is Required")]
        public int Trial_No { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        public string FullName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Dob is Required")]
        public Nullable<System.DateTime> Dob { get; set; }

        [Display(Name = "Mobile Number")]

        [Required(ErrorMessage = "Mob is Required")]
        public string Mob { get; set; }

        [Display(Name = "Address")]

        [Required(ErrorMessage = "Address is Required")]
        public string Address1 { get; set; }

        [Display(Name = "Existing Condition")]

        [Required(ErrorMessage = "Existing Condition is Required")]
        public string Existing_Condition { get; set; }

        [Display(Name = "Registration date")]
        [DataType(DataType.Date)]
        // [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Invalid date format.")]
        // [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Registration date is Required")]
        public Nullable<System.DateTime> Registration_date { get; set; }

        public string Approve_Status { get; set; }

        public Nullable<int> Is_Active { get; set; }
    }
}