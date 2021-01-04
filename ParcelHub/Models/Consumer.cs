using Microsoft.AspNetCore.Identity;
using ParcelHub.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class Consumer
    {
        //Each Consumer might have >=0 addresses
        public bool ModelIsvalid { get; set; } = true;
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

        public DateTime DateRegisterd { get; set; }

        public DateTime DateTimeLastLogin { get; set; }

        public string WechatId { get; set; }

        public string MemeberShipId { get; set; }
    }
}
