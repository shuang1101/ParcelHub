using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParcelHub.Models;
using System;
using System.Linq;


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
            if (_dbContext.Database.GetPendingMigrations().Count() > 0)
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




            if (!_dbContext.Country.Any())
            {
                Country newZealand = new Country() { CountryName = "New Zealand" };
                Country Korea = new Country() { CountryName = "Korea" };
                Country China = new Country() { CountryName = "China" };
                Country Usa = new Country() { CountryName = "USA" };

                var NZ = _dbContext.Country.Add(newZealand);
                var KOREA = _dbContext.Country.Add(Korea);
                var CHINA = _dbContext.Country.Add(China);
                var USA = _dbContext.Country.Add(Usa);
                _dbContext.SaveChangesAsync().GetAwaiter().GetResult();

                var nz = NZ.Entity.Id;
                //var korea = KOREA.Entity.Id;
                //var china = CHINA.Entity.Id;
                //var usa = USA.Entity.Id;



                Region AucklandMetro = new Region() { CountryId = nz, RegionName = "Auckland Metro" };
                Region Whangarei_Hamilton_Metro = new Region() { CountryId = nz, RegionName = "Whangarei-Hamilton Metro" };
                Region CapeReinga_Wellington_Metro = new Region() { CountryId = nz, RegionName = "CapeReinga-Wellington Metro" };
                Region SouthIsland_Metro = new Region() { CountryId = nz, RegionName = "South Island Metro" };

                _dbContext.Region.Add(AucklandMetro);
                _dbContext.Region.Add(Whangarei_Hamilton_Metro);
                _dbContext.Region.Add(CapeReinga_Wellington_Metro);
                _dbContext.Region.Add(SouthIsland_Metro);
                _dbContext.SaveChangesAsync().GetAwaiter().GetResult();

            }

            var UserName = _configuration.GetValue<string>("SeedMasterInformation:UserName");
            var Password = _configuration.GetValue<string>("SeedMasterInformation:Password");

            var master = new ApplicationUser()
            {
                UserName = UserName,
                EmailConfirmed = true,
                Email = UserName,
                SPWarehouseModelIdIfUserIsAdmin = 999
            };

            _userManager.CreateAsync(master, Password).GetAwaiter().GetResult();

            var masterRole = _userManager.FindByEmailAsync(UserName).GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(masterRole, "Master").GetAwaiter().GetResult();

            var consumerMaster = new Consumer()
            {
                LastName = "Master",
                FirstName = "SuperUser",
                Email = UserName,
                ApplicationUserId = masterRole.Id,
                DateRegisterd = DateTime.Now,
                MemeberShipId = "Master"
            };

            _dbContext.Consumer.Add(consumerMaster);

            _dbContext.SaveChangesAsync().GetAwaiter().GetResult();


        }
    }
}
