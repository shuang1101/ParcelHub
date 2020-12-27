using Microsoft.AspNetCore.Identity;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountRepository(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateUserAsync(InValidUser invalidUser)
        {
            var user = new IdentityUser()
            {
                Email = invalidUser.Email,
                UserName = invalidUser.Email
            };
            var result = await _userManager.CreateAsync(user, invalidUser.Password);
            return result;
        }
        public async Task<SignInResult> PasswordSignInAsync(LoginUser loginUser)
        {
           var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, loginUser.RememberMe, false);

            return result;
        }

        public async Task SignOutAsync()
        {
           await _signInManager.SignOutAsync();
            
        }

      

    }
}
