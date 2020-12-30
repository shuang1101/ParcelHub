using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;
using ParcelHub.ServiceRepository;

namespace ParcelHub.Controllers
{
    public class ConsumersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserSerivce _userservice;
        private readonly ApplicationDbContext _context;

        public ConsumersController(UserManager<IdentityUser> userManager  ,IUserSerivce userservice, ApplicationDbContext context)
        {
            _userManager = userManager;
            _userservice = userservice;
            _context = context;
        }

        // GET: Consumers
        public async Task<IActionResult> Index()
        {
            var consumer = _context.Consumer
                .Where(consumer => consumer.IdentityUserId == _userservice.GetUserId());

            return View(await consumer.ToListAsync());
        }

        // GET: Consumers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer
                .FirstOrDefaultAsync(m => m.IdentityUserId == id);
            if (consumer == null)
            {
                return NotFound();
            }

            return View(consumer);
        }

      
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumer = await _context.Consumer.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }
            return View(consumer);
        }

        // POST: Consumers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string? id)
        {
            if (id ==null)
            {
                return NotFound();
            }
            var consumerToUpdate =  _context.Consumer.Find(id);
            if (await TryUpdateModelAsync<Consumer>
                (consumerToUpdate,"",c=>c.LastName
                ,c=>c.FirstName,c=>c.MobileNumber))
            {
                try
                {
                    await _context.SaveChangesAsync();
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumerExists(consumerToUpdate.IdentityUserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consumerToUpdate);
        }

        private bool ConsumerExists(string id)
        {
            return _context.Consumer.Any(e => e.Email == id);
        }

        // GET: Consumers/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var consumer = await _context.Consumer
        //        .FirstOrDefaultAsync(m => m.Email == id);
        //    if (consumer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(consumer);
        //}

        //// POST: Consumers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var consumer = await _context.Consumer.FindAsync(id);
        //    _context.Consumer.Remove(consumer);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        // GET: Consumers/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Consumers/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,MobileNumber,Email,Password,IsValid")] Consumer consumer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(consumer);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(consumer);
        //}

        // GET: Consumers/Edit/5

    }
}
