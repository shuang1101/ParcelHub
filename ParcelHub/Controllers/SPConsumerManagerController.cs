using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;

namespace ParcelHub.Controllers
{
    public class SPConsumerManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPConsumerManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPConsumerManager
        public async Task<IActionResult> Index()
        {
            // hide seeduser ==master Id==1
            return View(await _context.Consumer.Where(m => m.Id != 1).ToListAsync());
        }

        public async Task<IActionResult> IndexViewOnly()
        {


            return View(await _context.Consumer.Where(m => m.Id != 1).ToListAsync());
        }

        public IActionResult CheckAddress(string id)
        {
            var consumerAddress = _context.ConsumerAddress.Where(ca => ca.ApplicationUserId == id).ToList();
            return View(consumerAddress);

        }
        public IActionResult BanConsumer(string id)
        {
            
            var consumer = _context.Consumer.FirstOrDefault(ca => ca.ApplicationUserId == id);

            if (consumer == null)

            {
                return NotFound();
            }

            return View(consumer);

        }
        [HttpPost]
        public async Task<IActionResult> BanConsumer(Consumer consumer)
        {

            var uID = consumer.ApplicationUserId;

            var user = _context.Users.Find(uID);

            var consumerUser = _context.Consumer.FirstOrDefault(c => c.ApplicationUserId == uID);

            user.IsValidUser = consumer.ModelIsvalid;

            _context.Users.Update(user);
            
            await _context.SaveChangesAsync();

            consumerUser.ModelIsvalid = consumer.ModelIsvalid;

            _context.Consumer.Update(consumerUser);

            await _context.SaveChangesAsync();

            ViewBag.BanConsumer = 1;

            return View(consumerUser);

        }
    }
}
