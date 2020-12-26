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
        public List<ConsumerAddress> ConsumerAddresses { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MobileNumber { get; set; }
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsValid { get; set; }
    }
}
