using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class SPWarehouseModel
    {
       
        [Key]
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }

        public bool ModelIsvalid { get; set; }

        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please fill address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        public string CountryOfWarehouseModelCountryName { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }
        public string ReceiverName { get; set; }

        public bool AirService { get; set; } = false;
        public bool LandService { get; set; } = false;
        public bool OceanFreightService { get; set; } = false;

      public int RegionListId { get; set; }
    }
}
