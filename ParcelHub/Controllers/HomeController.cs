using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;
using ParcelHub.ServiceRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserSerivce _userSerivce;
        private readonly IEmailService _emailService;

        public HomeController(IUserSerivce userSerivce, IEmailService emailService, ILogger<HomeController> logger,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userSerivce = userSerivce;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
           // ViewBag.IsLogIn = _userSerivce.IsAuthenticated();
    
          //  ViewBag.ConsumerName = _userSerivce.GetUserName();
            _logger.LogInformation("home page visit");
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    

    }
}
