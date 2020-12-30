using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcelHub.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Controllers
{
    public class SPParcelManagerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SPParcelManagerController( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // pass all parcel information
        public async Task< IActionResult> Index()
        {
           
            return View( await _dbContext.Parcel.ToListAsync());
        }
    }
}
