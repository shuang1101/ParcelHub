using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.DatabaseConnection
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public DbInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public void InitializeDB()
        {
            if (_dbContext.Database.GetPendingMigrations().Count()>0)
            {
                _dbContext.Database.Migrate();
            }

            if (!_dbContext.Roles.Any())
            {
                _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" }).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole() { Name = "Master" }).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            if (!_dbContext.CountryOfWarehouseModel.Any())
            {
                CountryOfWarehouseModel newZealand = new CountryOfWarehouseModel() {CountryName="New Zealand" };
                CountryOfWarehouseModel Korea = new CountryOfWarehouseModel() { CountryName = "Korea" };
                CountryOfWarehouseModel China = new CountryOfWarehouseModel() { CountryName = "China" };
                CountryOfWarehouseModel USA = new CountryOfWarehouseModel() { CountryName = "USA" };

                _dbContext.CountryOfWarehouseModel.Add(newZealand);
                _dbContext.CountryOfWarehouseModel.Add(Korea);
                _dbContext.CountryOfWarehouseModel.Add(China);
                _dbContext.CountryOfWarehouseModel.Add(USA);
                _dbContext.SaveChangesAsync().GetAwaiter().GetResult();

            }


            var UserName = _configuration.GetValue<string>("SeedMasterInformation:UserName");
            var Password = _configuration.GetValue<string>("SeedMasterInformation:Password");

            var master = new ApplicationUser()
            {
                UserName = UserName,
                EmailConfirmed = true,
                Email = UserName,
                SPWarehouseModelIdIfUserIsAdmin=999
            };

             _userManager.CreateAsync(master, Password).GetAwaiter().GetResult();

            var masterRole =  _userManager.FindByEmailAsync(UserName).GetAwaiter().GetResult();

             _userManager.AddToRoleAsync(masterRole, "Master").GetAwaiter().GetResult();

            var consumerMaster = new Consumer()
            {
                LastName = "Master",
                FirstName = "SuperUser",
                Email = UserName,
                ApplicationUserId = masterRole.Id,
                DateRegisterd=DateTime.Now,
                MemeberShipId="Master"
            };

            _dbContext.Consumer.Add(consumerMaster);

             _dbContext.SaveChangesAsync().GetAwaiter().GetResult();


        }
    }
}
