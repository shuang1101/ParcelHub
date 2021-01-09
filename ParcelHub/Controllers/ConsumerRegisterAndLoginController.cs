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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public ConsumerRegisterAndLoginController(ApplicationDbContext dbcontect, UserManager<ApplicationUser> userManager, IEmailService emailService, IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
            _dbcontect = dbcontect;
            _userManager = userManager;
            _emailService = emailService;
        }




        [Route("signup-email")]
        public IActionResult ConsumerSignUpByEmail()
        {
            return View();
        }





        [Route("signup-number")]
        public IActionResult ConsumerSignUpByNumber()
        {
            return View();
        }


        public IActionResult termAndConditions()
        {
            return View();
        }


        [Route("signup-email")]
        [HttpPost]
        public async Task<IActionResult> ConsumerSignUpByEmail(SignUpUser User)
        {

            if (ModelState.IsValid)
            {
                if (User.ConfirmTAndC == false)
                {
                    ModelState.AddModelError("", "Please read and accept Kiwi parcel term and conditions");
                    return View(User);
                }

                var result = await _accountRepo.CreateUserAsync(User);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(User);
                }

                ModelState.Clear();
            }
            return RedirectToAction("AfterRegister");
        }

        public IActionResult AfterRegister()
        {
            return View();
        }




        [Route("signup-number")]
        [HttpPost]

        public async Task<IActionResult> ConsumerSignUpByNumber(SignUpUser User)
        {

            if (ModelState.IsValid)
            {
                if (User.ConfirmTAndC == false)
                {
                    ModelState.AddModelError("", "Please read and accept Kiwi parcel term and conditions");
                    return View(User);
                }

                var result = await _accountRepo.CreateUserAsync(User);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(User);
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
                    // if found login to be staff, will signout and redirect to backend, because the adminEntity Does not apply to the consumer Entity

                    var user = await _userManager.FindByEmailAsync(loginUser.Email);

                    if (user.SPWarehouseModelIdIfUserIsAdmin > 0)
                    {
                      await  _accountRepo.SignOutAsync();

                        return RedirectToAction("SPLogin", "SPLogin");
                    }

                    // if client that is banned form site, will redirect to banned

                    if (user.IsValidUser == false)
                    {
                        await _accountRepo.SignOutAsync();

                        return RedirectToAction("NeedHelpPage", "ConsumerRegisterAndLogin");
                    }

                    // record last login time

                   var consumer = _dbcontect.Consumer.FirstOrDefault(c => c.ApplicationUserId == user.Id);
                    consumer.DateTimeLastLogin = DateTime.Now;
                    _dbcontect.Consumer.Update(consumer);
                   await _dbcontect.SaveChangesAsync();

                    return RedirectToAction("Index", "ConsumerHome");
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
            return RedirectToAction("Index", "Home");
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
        private async Task<IActionResult> CopyIdentityAsConsumer(ApplicationUser user, string uid)
        {

            var Exist = _dbcontect.Consumer
                .Where(user => user.Email == uid);

            if (user != null)
            {
                Consumer consumer = new Consumer()
                {
                    ApplicationUserId = user.Id,
                    Email = user.Email,
                    LastName = "Please update",
                    FirstName = "name",
                    DateRegisterd = DateTime.Now,

                };


                var result = _dbcontect.Add(consumer);
                await _dbcontect.SaveChangesAsync();
                result.Entity.MemeberShipId = (1000 + result.Entity.Id).ToString();
                await _dbcontect.SaveChangesAsync();


            }

            return null;

        }

        public IActionResult NeedHelpPage()
        {
            return View();
        }


        public IActionResult ReSendEmail()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

       
    }
}
