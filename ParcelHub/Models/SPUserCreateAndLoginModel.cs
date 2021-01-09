using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class SPUserCreateAndLoginModel
    {
        [Required(ErrorMessage ="Please enter user name")]
        public string UserName { get; set; }


        public string ApplicationUserId { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public int SPWarehouseModelId { get; set; }

        [ForeignKey("SPWarehouseModelId")]
        public SPWarehouseModel SPWarehouseModel { get;set;}

        public bool IsValid { get; set; }

        [Required(ErrorMessage ="Please provide authority level")]
        public string Role { get; set; }
       [Required(ErrorMessage ="require password")]
       [DataType(DataType.Password)]
       [Compare("ConfirmPassword",ErrorMessage ="Password does not match")]
        public string Password { get; set; }
        [Required(ErrorMessage ="require confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
