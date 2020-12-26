using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class Parcel
    {
        [Key]
        public int Id { get; set; }

        // Each Shippment contains many Parcels
        // Each parcel can only be in one and only one Shippment

        public int ShippmentId { get; set; }
        //public Shippment Shippment { get; set; }

        //Unable to determine the relationship represented by navigation 
        //    'Parcel.Shippment' of type 'Shippment'. Either 
        //    manually configure the relationship, or ignore
        //    this property using the '[NotMapped]' attribute
        //or by using 'EntityTypeBuilder.Ignore' in 'OnModelCreating'.


        public string OriginTrackingNumber { get; set; }
        public string Description { get; set; }
        public float EstimateWeight { get; set; }
        public float ActualWeight { get; set; }
        public float EstimateVolume { get; set; }
        public float ActualVolume { get; set; }
        public float ItemValue { get; set; }
        public string Reference { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Inbound { get; set; }
    }
}
