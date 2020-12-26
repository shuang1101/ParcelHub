using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class ShippingContainer
    {
        public int Id { get; set; }
        public string ContainerNumber { get; set; }
        public DateTime ETA { get; set; }
        public DateTime ETD { get; set; }
        public string Vessel { get; set; }
    }
}
