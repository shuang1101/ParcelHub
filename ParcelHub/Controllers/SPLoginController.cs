using Microsoft.AspNetCore.Mvc;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParcelHub.ServiceRepository;
using Microsoft.AspNetCore.Identity;

namespace ParcelHub.Controllers
{
    public class SPLoginController : Controller
    {
        private readonly IAccountRepository _accountRepo;



        public SPLoginController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }
        [HttpGet]
        [Route("/backend/admin-login")]
        public IActionResult SPLogin()
        {
            return View();
        }
        [Route("/backend/admin-login", Name = "admin-login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SPLogin(LoginUser user)
        {

            if (ModelState.IsValid)
            {
                
                    var result = await _accountRepo.PasswordSignInAsync(user);
                    if (result.Succeeded)
                    {
                        
                        ModelState.Clear();
                        return RedirectToAction("Index", "SPHome");
                    }
                    if (!result.Succeeded)
                    {
                        if (result.IsNotAllowed)
                        {
                            ModelState.AddModelError("", "Account not verified!");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Crediential error!");
                        }
                    
                }
               

            }

            return View();
        }
    }
}
