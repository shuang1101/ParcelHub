using Microsoft.AspNetCore.Identity;
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

        public string SPTackingNumber { get; set; }
        public string PackageLabelBarCode { get; set; } // = Tracking + box number if have multi box in one shippment

        // each parcel has a consumerID
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public string OriginCourierCompany { get; set; }
        public string OriginTrackingNumber { get; set; }

        public string CountryOfOrigin { get; set; }
        public int DestinationAddressId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]

        public string EstimateWeight { get; set; }


        [Required]

        public string EstimateVolume { get; set; }


        public float ActualVolume { get; set; }

        public float ActualWeight { get; set; }

        [Required]

        public string TotalValue { get; set; }

        public string Reference { get; set; }
        public string TransitStatus { get; set; } = "Awaiting inbound";
        public string DestinationDeliverMethod { get; set; }

        public string NumberOfUnits { get; set; }

        //Record the time when origin scanned the parcel
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeInboundOrigin { get; set; }

        //Recrod the time when destination scanned the parcel
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeArriveInDestination { get; set; }

        //Recrod the time when client created the job
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeJobCreated { get; init; } = DateTime.Now;

        //Recrod the time when client last edit the job
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeJobLastEdit { get; set; }

        public bool ModelIsvalid { get; set; } = true;

    }
}
