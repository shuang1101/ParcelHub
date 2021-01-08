using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class Region
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public string RegionName { get; set; }
    }
}
