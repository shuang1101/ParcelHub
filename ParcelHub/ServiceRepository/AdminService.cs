using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public class AdminService : IAdminService
    {
      
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserSerivce _userSerivce;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser>  _userManager;

        public AdminService(IHttpContextAccessor httpContext, IUserSerivce userSerivce, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            
            _httpContext = httpContext;
            _userSerivce = userSerivce;
            _dbContext = dbContext;
            _userManager = userManager;

        }

        public int GetAdminSPWarehouseId()
        {
            var Uid = _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u=>u.Id==Uid);
            if (user is null)
            {
                return -10;
            }
            return user.SPWarehouseModelIdIfUserIsAdmin;
        }


        public async Task<IdentityResult> ChangePasswordForSPAdmin(ApplicationUser spAdmin, string newPassword)
        {
            await _userManager.RemovePasswordAsync(spAdmin);
          var result = await _userManager.AddPasswordAsync(spAdmin, newPassword);
            return result;
        }

        public async Task<IdentityResult> AddSPUserToRole(ApplicationUser user, string role)
        {
           var result = _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();

            return result;
        }

        
    }
}
