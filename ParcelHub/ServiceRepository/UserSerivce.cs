using Microsoft.AspNetCore.Http;
using ParcelHub.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public class UserSerivce : IUserSerivce
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _dbContext;

        public UserSerivce(IHttpContextAccessor httpContext,ApplicationDbContext dbContext)
        {
            _httpContext = httpContext;
            _dbContext = dbContext;
        }
        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
        public string GetUserEmail()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.Email);
        }
        public  string?  GetUserName()
        {
            string userId = _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _dbContext.Consumer.Find(userId);
            if (result == null)
            {
                return "Error";
            }
            return result.LastName + " "+result.FirstName;
        }
    }
}
