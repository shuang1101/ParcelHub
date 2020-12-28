﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParcelHub.Controllers;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.DatabaseConnection
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
 
        public DbSet<ConsumerAddress> ConsumerAddress { get; set; }

        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<Parcel> Parcel { get; set; }
        public DbSet<ServiceProviderUser> ServiceProviderUser { get; set; }
        public DbSet<ShippingContainer> ShippingContainer { get; set; }

        public DbSet<Shippment> Shippment { get;set;}

        public DbSet<ParcelHub.Models.HomePageNews> HomePageNews { get; set; }
       
        public DbSet<ParcelHub.Models.InValidUser> InValidUser { get; set; }
       
        public DbSet<ParcelHub.Models.LoginUser> LoginUser { get; set; }
       
        public DbSet<ParcelHub.Models.ChangePasswordUserModel> ChangePasswordUserModel { get; set; }
       
        public DbSet<ParcelHub.Models.PackagePerClient> PackagePerClient { get; set; }
        
    }
}
