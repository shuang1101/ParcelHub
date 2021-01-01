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
    public class ConsumerParcelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserSerivce _userService;

        public ConsumerParcelsController(ApplicationDbContext context,  IUserSerivce userService)//
        {
            _context = context;
            _userService = userService;
        }

        // GET: Parcels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parcel.Include(p => p.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Parcels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcel
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // GET: Parcels/Create
        public IActionResult Create()
        {
            // ViewBag.Name = _userService.GetUserName();
            ViewBag.Name = _userService.GetUserName();
            return View();
        }

        // POST: Parcels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddMoreParcelModel addMoreParcelModel)
        {
            if (true)//ModelState.IsValid
            {
                string userId = _userService.GetUserId();
            foreach (AddMoreParcelModel parcel in addMoreParcelModel.AddMoreParcel)
            {
                Parcel eachParcel = new Parcel()
                {
                    ShippmentId = parcel.ShippmentId,
                    IdentityUserId = userId,
                    OriginTrackingNumber = parcel.OriginTrackingNumber,
                    Description = parcel.Description,
                    EstimateWeight = parcel.EstimateWeight,
                    EstimateVolume = parcel.EstimateVolume,
                    TotalValue = parcel.TotalValue,
                    Reference = parcel.Reference,
                    DestinationDeliverMethod = parcel.DestinationDeliverMethod,
                    NumberOfUnits = parcel.NumberOfUnits
                };
                _context.Add(eachParcel);
            }
            await _context.SaveChangesAsync();
                ViewBag.Name = _userService.GetUserName();
                return RedirectToAction(nameof(Index));
            }
            

           ViewBag.Name = _userService.GetUserName();
            return  View();
        }

        // GET: Parcels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcel.FindAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.IdentityUserId);
            return View(parcel);
        }

        // POST: Parcels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShippmentId,SPTackingNumber,PackageLabelBarCode,IdentityUserId,OriginTrackingNumber,Description,EstimateWeight,EstimateVolume,ActualVolume,ActualWeight,ItemValue,Reference,TransitStatus,DestinationDeliverMethod,Amount,Inbound,ArriveInDestination,JobCreated,JobLastEdit")] Parcel parcel)
        {
            if (id != parcel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelExists(parcel.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.IdentityUserId);
            return View(parcel);
        }

        // GET: Parcels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcel
                .Include(p => p.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // POST: Parcels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcel = await _context.Parcel.FindAsync(id);
            _context.Parcel.Remove(parcel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelExists(int id)
        {
            return _context.Parcel.Any(e => e.Id == id);
        }
    }
}
