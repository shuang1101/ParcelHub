using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class ChangePasswordUserModel
    {
        public int Id { get; set; }
        [Required,DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }
        [Display(Name ="New Password")]
        [Required,DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name ="ConfirmPassword")]
        [Required,DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="password not matching")]
        public string ConfirmNewPassword { get; set; }
    }
}
