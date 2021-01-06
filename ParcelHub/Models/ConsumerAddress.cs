
using Microsoft.AspNetCore.Identity;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class ConsumerAddress
    {
        public string NameOfReceiver { get; set; }
        public string NameOfMyAddress { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Key]
        public int Id { get; init; }
        public int CountryOfWarehouseModelIdAtDestination { get; set; }
      
        public string State { get; set; }
        public string City { get; set; }

        public string Suburb { get; set; }
        public string StreetAddress { get; set; }

        public string PostCode { get; set; }
        public bool ModelIsvalid { get; set; } = true;


    }
}