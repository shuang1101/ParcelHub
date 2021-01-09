using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class RegionList
    {
        public int Id { get; set; }

        public int regionId { get; set; }

        public Region Region { get; set; }

        public int SPWarehouseModelId { get; set; }
        public SPWarehouseModel SPWarehouseModel { get; set; }
    }
}
