using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class CreateParcelModel
    {
       
        
            public List<CreateParcelModel> AddMoreParcel { get; set; }
            public int Id { get; set; }

            // Each Shippment contains many Parcels
            // Each parcel can only be in one and only one Shippment

            public int ShippmentId { get; set; }
        
        public string ShippingCompanyAtOrigin { get; set; }
            public string SPTackingNumber { get; set; }
            public string PackageLabelBarCode { get; set; } // = Tracking + box number if have multi box in one shippment

            // each parcel has a consumerID
            public string ApplicationUserId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }
            public string OriginTrackingNumber { get; set; }

            public ConsumerAddress consumerAddress { get;set; }
        public string CountryOfOrigin { get; set; }
            [Required]
           
            public string Description { get; set; }

            [Required]

            public float EstimateWeight { get; set; }


            [Required]

            public float EstimateVolume { get; set; }


            public float ActualVolume { get; set; }

            public float ActualWeight { get; set; }

            [Required]

            public float TotalValue { get; set; }

            public string Reference { get; set; }
            public string TransitStatus { get; set; }
            public string DestinationDeliverMethod { get; set; }

            public int NumberOfUnits { get; set; }

            //Record the time when origin scanned the parcel
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
            public DateTime DateTimeInboundOrigin { get; set; }

            //Recrod the time when destination scanned the parcel
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
            public DateTime DateTimeArriveInDestination { get; set; }

            //Recrod the time when client created the job
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
            public DateTime DateTimeJobCreated { get; set; }

            //Recrod the time when client last edit the job
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
            public DateTime DateTimeJobLastEdit { get; set; }



        
    }
}
