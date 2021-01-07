using Microsoft.EntityFrameworkCore;
using ParcelHub.Models;

namespace ParcelHub.DatabaseConnection
{
    public interface IApplicationDbContext
    {
        DbSet<Consumer> Consumer { get; set; }
        DbSet<ConsumerAddress> ConsumerAddress { get; set; }
        DbSet<CountryOfWarehouseModel> CountryOfWarehouseModel { get; set; }
        DbSet<HomePageNews> HomePageNews { get; set; }
        DbSet<Invoice> Invoice { get; set; }
        DbSet<Parcel> Parcel { get; set; }
        DbSet<ShippingContainer> ShippingContainer { get; set; }
        DbSet<Shippment> Shippment { get; set; }
        DbSet<SPUserModel> SPUserModel { get; set; }
        DbSet<SPWarehouseModel> SPWarehouseModel { get; set; }
    }
}