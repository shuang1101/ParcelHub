using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParcelHub.Models;
using ParcelHub.ServiceRepository;

namespace ParcelHub.Controllers
{
    //
    public class ConsumerRegisterAndLoginController : Controller
    {
        private readonly IAccountRepository _accountRepo;

        public ConsumerRegisterAndLoginController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
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
                ModelState.AddModelError("", "Crediential Error");
            }
            return View();
        }

        // LogOut
        public async Task<IActionResult> ConsumerLogOut()
        {
            await _accountRepo.SignOutAsync();
            return RedirectToAction("ConsumerSignUp", "ConsumerRegisterAndLogin");
        }
    }
}
