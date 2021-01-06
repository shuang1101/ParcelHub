using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class CountryOfWarehouseModel
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }

    }
}
