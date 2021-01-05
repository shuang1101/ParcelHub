using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParcelHub.Controllers;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.DatabaseConnection
{
    public  class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ConsumerAddress> ConsumerAddress { get; set; }

        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        public DbSet<Parcel> Parcel { get; set; }
        public DbSet<SPWarehouseModel> SPWarehouseModel { get; set; }
        public DbSet<ShippingContainer> ShippingContainer { get; set; }

        public DbSet<Shippment> Shippment { get;set;}

        public DbSet<HomePageNews> HomePageNews { get; set; }
        public DbSet<CountryOfWarehouseModel> CountryOfWarehouseModel { get; set; }
        public DbSet<SPUserModel> SPUserModel { get; set; }
        

    }
}
