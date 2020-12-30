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

        public string SPTackingNumber { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        //BROWN
        // Introducing FOREIGN KEY constraint 'FK_Shippment_parcel_Id' 
        //on table 'Shippment' may cause cycles or multiple cascade paths.
        //    Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, 
        //    or modify other FOREIGN KEY constraints.
        //    Could not create constraint or index.See previous errors.
        //[ForeignKey("Id")]
        //public Parcel Parcel { get; set; }


        public string ServiceProviderUserId { get; set; }


        public string Origin { get; set; }

        public string Destination { get; set; }
      

        public int ShippingContainerId { get; set; }

       
        public bool RequiredInsurance { get; set; }
       
}
}
