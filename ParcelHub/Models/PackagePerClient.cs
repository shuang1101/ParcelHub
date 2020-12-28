using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class PackagePerClient
    {
        public int ShippmentId { get; set; }

        public int ConsumerId { get; set; }
        public int ParcelId { get; set; }

        public int Id { get; set; }
    }
}
