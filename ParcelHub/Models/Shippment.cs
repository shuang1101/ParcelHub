using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Models
{
    public class Shippment
    {


        // Each Shippment contains many Parcels
        // Each parcel can only be in one and only one Shippment
        [Key]
        public int Id { get; set; }

        public int OriginSPWarehouseModelId { get; set; }


        public int DestinatioSPWarehouseModelnId { get; set; }

        public int ConsumerAddressId { get; set; }
        [ForeignKey("ConsumerAddressId")]
        public virtual ConsumerAddress ConsumerAddress { get; set; }

        public string SPTackingNumber { get; set; }

        public string ApplicationUserId { get; set; }

        public string MemberShipId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        

     
        public int ShippingContainerId { get; set; }

       
        public bool RequiredInsurance { get; set; }

        public float TotalValue { get; set; }

        public bool RequireDelivery { get; set; }
        public bool ModelIsvalid { get; set; } = true;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateTimeJobCreated { get; init; } = DateTime.Now;
    }
}
