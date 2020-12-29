using Microsoft.AspNetCore.Identity;
using ParcelHub.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class Consumer
    {
        //Each Consumer might have >=0 addresses
       
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string MobileNumber { get; set; }
       
        public string Email { get; set; }
         [Key]
        public string IdentityUserId { get; set; }

        public string Password { get; set; }
        public bool IsValid { get; set; }

        public DateTime DateRegisterd { get; set; }

        public DateTime DateTimeLastLogin { get; set; }
    }
}
