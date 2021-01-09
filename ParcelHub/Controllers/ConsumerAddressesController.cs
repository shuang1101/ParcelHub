using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcelHub.DatabaseConnection;
using ParcelHub.ServiceRepository;
using ParcelHub.Models;

namespace ParcelHub.Controllers
{
    public class ConsumerAddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserSerivce _userService;

        public ConsumerAddressesController(ApplicationDbContext context, IUserSerivce userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: ConsumerAddresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConsumerAddress.
                Where(address => address.ApplicationUserId == _userService.GetUserId())
                .Where(add => add.ModelIsvalid == true);



            return View(await applicationDbContext.ToListAsync());
        }


        // GET: ConsumerAddresses/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: ConsumerAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameOfReceiver,NameOfMyAddress,Country,RegionId,CountryId,State,Suburb,City,StreetAddress,PostCode")] ConsumerAddress consumerAddress)
        {
            if (ModelState.IsValid)
            {
                consumerAddress.ApplicationUserId = _userService.GetUserId();

                _context.Add(consumerAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consumerAddress);
        }

        // GET: ConsumerAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumerAddress = await _context.ConsumerAddress.FindAsync(id);
            if (consumerAddress == null)
            {
                return NotFound();
            }
            return View(consumerAddress);
        }

        // POST: ConsumerAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,State,City,NameOfMyAddress,NameOfReceiver,CountryId,RegionId,Suburb,StreetAddress,PostCode")] ConsumerAddress consumerAddress)
        {
            if (id != consumerAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    consumerAddress.ApplicationUserId = _userService.GetUserId();
                    _context.Update(consumerAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumerAddressExists(consumerAddress.Id))
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
            return View(consumerAddress);
        }

        // GET: ConsumerAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumerAddress = await _context.ConsumerAddress
                .Where(address => address.ApplicationUserId == _userService.GetUserId())
                .Where(add => add.ModelIsvalid == true)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumerAddress == null)
            {
                return NotFound();
            }

            return View(consumerAddress);
        }

        // POST: ConsumerAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumerAddress = await _context.ConsumerAddress.FindAsync(id);
            consumerAddress.ModelIsvalid = false;
            _context.Update(consumerAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumerAddressExists(int id)
        {
            return _context.ConsumerAddress.Any(e => e.Id == id);
        }

        [Route("/data/getRegionForConsumer")]
        [HttpPost]
        public JsonResult GetRegion([FromBody] string s)
        {
            if (s == null)
            {
                return null;
            }
            List<KeyValuePair<int, string>> data = new List<KeyValuePair<int, string>>();
            try
            {
                var Id = Int32.Parse(s);
                

                foreach (var region in _context.Region.Where(r => r.CountryId == Id).ToList())
                {
                    data.Add(
                        new KeyValuePair<int, string>(region.Id, region.RegionName)
                        );
                }

            }
            catch
            {
                Exception ex;
            }

            return Json(data);

        }
    }
}
