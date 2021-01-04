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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public DbInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
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

            var UserName = _configuration.GetValue<string>("SeedMasterInformation:UserName");
            var Password = _configuration.GetValue<string>("SeedMasterInformation:Password");

            var master = new IdentityUser()
            {
                UserName = UserName,
                EmailConfirmed = true,
                Email = UserName,
            };

             _userManager.CreateAsync(master, Password).GetAwaiter().GetResult();

            var masterRole =  _userManager.FindByEmailAsync(UserName).GetAwaiter().GetResult();

             _userManager.AddToRoleAsync(masterRole, "Master").GetAwaiter().GetResult();

            var consumerMaster = new Consumer()
            {
                LastName = "Master",
                FirstName = "SuperUser",
                Email = UserName,
                IdentityUserId = masterRole.Id,
                DateRegisterd=DateTime.Now,
                MemeberShipId="Master"
            };

            _dbContext.Consumer.Add(consumerMaster);

             _dbContext.SaveChangesAsync().GetAwaiter().GetResult();


        }
    }
}
