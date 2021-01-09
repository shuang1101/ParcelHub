using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            var result = _dbContext.Consumer.FirstOrDefault(user=>user.ApplicationUserId==userId);
            if (result == null)
            {
                return "Error";
            }
            return result.LastName + " "+result.FirstName;
        }

        public string GetUserMemberId()
        {


          string id =  _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id != null && _dbContext.Users.Find(id).SPWarehouseModelIdIfUserIsAdmin > 0)
            {
                return "Hi Dear Staff";
            }

            var consumer = _dbContext.Consumer.FirstOrDefault(consumer=>consumer.ApplicationUserId==id);
           
            return consumer.MemeberShipId;
        }
    }
}
