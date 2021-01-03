using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class ServiceProviderUser
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string StreeAddress { get; set; }
        public string PostCode { get; set; }
        public bool ModelIsvalid { get; set; }
    }
}
