using Microsoft.AspNetCore.Mvc;
using ParcelHub.Models;
using ParcelHub.ServiceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Controllers
{

    // Change Password
    // Display consumer profile page
    //display detail update page
    public class ConsumerProfileController : Controller
    {
        private readonly IUserSerivce _userService;
        private readonly IAccountRepository _accountRepo;

        public ConsumerProfileController(IUserSerivce userService, IAccountRepository accountRepo)
        {
            _userService = userService;
            _accountRepo = accountRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("change-password")]
        public  IActionResult ChangePassword()
        {
            return View();
        }


        // change password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepo.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    ViewBag.Flag = true;
                    return View();
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            ViewBag.Flag = false;
            return View(model);
        }
    }
}
