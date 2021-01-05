using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IUserSerivce _userService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(
            IEmailService emailService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IUserSerivce userService,
            RoleManager<IdentityRole> roleManager
            
            )
        {
            _configuration = configuration;
            _userService = userService;
                        _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
           
        }

        // Create User + send varification email
        public async Task<IdentityResult> CreateUserAsync(SignUpUser signUpUser)
        {
            var user = new ApplicationUser()
            {
                Email = signUpUser.Email,
                UserName = signUpUser.Email,
                PhoneNumber = signUpUser.MobileNumber
            };
            // pass userModel + password => save in DB
            var result = await _userManager.CreateAsync(user, signUpUser.Password);

            // If Succeed, send client email ask for varificaiton email address
            if (result.Succeeded)
            {
                if (user.PhoneNumberConfirmed == true)
                {
                    return result;
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (!string.IsNullOrEmpty(token))
                {
                    // replace actual token and username with placeholders
                    UserEmailOption userEmailOption = CompleteReturnString(user, token);
                    //send Verification Email
                    _ = _emailService.SendConsumerAccountVerification(userEmailOption);
                }



            }
            return result;
        }


        public async Task<SignInResult> PasswordSignInAsync(LoginUser loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, loginUser.RememberMe, false);

            return result;
        }

        // confirm if uid and token matches 
        public async Task<IdentityResult> ConfirmVarification(string uid, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(await _userManager.FindByNameAsync(uid), token);
            return result;
        }



        // User SignOut
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();

        }


        // complete the content in Varification email

        private UserEmailOption CompleteReturnString(ApplicationUser user, string token)
        {
            string appDomain = _configuration
                       .GetSection("EmailVerification:AppDomain").Value;

            string EmailConfirmation = _configuration
                .GetSection("EmailVerification:EmailConfirmation").Value;

            string userEmail = user.Email;
            UserEmailOption userEmailOption = new UserEmailOption
            {

                Receiver = new List<string> { userEmail },
                PlaceHolder = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string,string>
                            ("{{Link}}",string.Format(appDomain + EmailConfirmation,userEmail,token)),
                            new KeyValuePair<string,string>("{{UserName}}",userEmail)
                        }


            };
            return userEmailOption;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordUserModel model)
        {

            string Id = _userService.GetUserId();

            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmNewPassword);


            return result;
        }

       
       
    }
}
