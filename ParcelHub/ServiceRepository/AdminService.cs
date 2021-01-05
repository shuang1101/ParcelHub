using Microsoft.AspNetCore.Http;
using ParcelHub.DatabaseConnection;
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

        public AdminService(IHttpContextAccessor httpContext, IUserSerivce userSerivce, ApplicationDbContext dbContext)
        {
            _httpContext = httpContext;
            _userSerivce = userSerivce;
            _dbContext = dbContext;
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



    }
}
