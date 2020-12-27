using Microsoft.AspNetCore.Identity;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Controllers
{
    public class ConsumerAddress
    {
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public int Id { get; init; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }

        public string PostCode { get; set; }

   

    }
}
