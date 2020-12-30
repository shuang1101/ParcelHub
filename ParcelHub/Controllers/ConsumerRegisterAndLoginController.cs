using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;
using ParcelHub.ServiceRepository;

namespace ParcelHub.Controllers
{
    // Consumer reigster    =>     ConsumerSignUp GET+ POST
    // Consumer Login       =>     ConsumerLogIn  GET+POST
    // Consumer Logout      =>     ConsumerLogOut
    // Consumer Email Varification  =>ConfirmEmailVarification

    public class ConsumerRegisterAndLoginController : Controller
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ApplicationDbContext _dbcontect;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public ConsumerRegisterAndLoginController(ApplicationDbContext dbcontect, UserManager<IdentityUser> userManager, IEmailService emailService, IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
            _dbcontect = dbcontect;
            _userManager = userManager;
            _emailService = emailService;
        }

        [Route("signup")]
        public IActionResult ConsumerSignUp()
        {
            return View();
        }


        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> ConsumerSignUp(InValidUser invalidUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepo.CreateUserAsync(invalidUser);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(invalidUser);
                }

                ModelState.Clear();
            }
            return View();
        }


        // login GET
        [Route("login")]
        public IActionResult ConsumerLogIn()
        {
            return View();
        }


        // Log in POST
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> ConsumerLogIn(LoginUser loginUser)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountRepo.PasswordSignInAsync(loginUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Email not verified");
                }
                else
                {
                    ModelState.AddModelError("", "Crediential error");
                }
            }
            return View();
        }



        // LogOut
        public async Task<IActionResult> ConsumerLogOut()
        {
            await _accountRepo.SignOutAsync();
            return RedirectToAction("ConsumerSignUp", "ConsumerRegisterAndLogin");
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailVarification(string uid, string token)
        {

            ViewBag.IsLogIn = false;

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {

                var user = await _userManager.FindByEmailAsync(uid);
                if (user.EmailConfirmed == true)
                {
                    // if already varified => 2
                    ViewBag.Flag = 2;
                    return View();
                }
                token = token.Replace(" ", "+");

                var result = await _accountRepo.ConfirmVarification(uid, token);
                if (result.Succeeded)
                {
                    //if successfully varifed =>1
                    await CopyIdentityAsConsumer(user, uid);
                    ViewBag.Flag = 1;

                    return View(); ;
                }
            }
            //error => 0
            ViewBag.Flag = 0;
            return View();
        }

        // copy this to consumerTable
        private async Task<IActionResult> CopyIdentityAsConsumer(IdentityUser user, string uid)
        {

            var Exist = _dbcontect.Consumer
                .Where(user => user.Email == uid);

            if (user != null)
            {
                Consumer consumer = new Consumer()
                {
                    IdentityUserId = user.Id,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    LastName = "Please update",
                    FirstName = "name",
                    DateRegisterd = DateTime.Now
                };


                _dbcontect.Add(consumer);
                await _dbcontect.SaveChangesAsync();
            }

            return null;

        }
    }
}
