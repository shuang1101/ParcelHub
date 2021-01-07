using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;
using ParcelHub.ServiceRepository;


namespace ParcelHub.Controllers
{
    public class ConsumerHomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserSerivce _userService;

        public ConsumerHomeController(ApplicationDbContext context, IUserSerivce userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Parcels
        public async Task<IActionResult> Index()
        {
            var result = _context.Parcel
                .Where(parcel => parcel.ApplicationUserId == _userService.GetUserId());

            return View(await result.ToListAsync());
        }

        
    }
}
