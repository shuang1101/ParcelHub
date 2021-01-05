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

        //// GET: Parcels/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var parcel = await _context.Parcel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (parcel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(parcel);
        //}

        //// GET: Parcels/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Parcels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,OriginTrackingNumber,Description,EstimateWeight,EstimateVolume,ItemValue,Reference,DestinationDeliverMethod")] Parcel parcel)
        //{
        //    parcel.ApplicationUserId = _userService.GetUserId();

        //    parcel.DateTimeJobLastEdit = DateTime.Now;

        //    if (ModelState.IsValid)
        //    {

        //        _context.Add(parcel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(parcel);
        //}

        // GET: Parcels/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var parcel = await _context.Parcel.FindAsync(id);
        //    if (parcel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(parcel);
        //}

        //// POST: Parcels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ShippmentId,JobCreated,OriginTrackingNumber,Description,EstimateWeight,ActualWeight,EstimateVolume,ActualVolume,ItemValue,Reference,TransitStatus,DestinationDeliverMethod")] Parcel parcel)
        //{
        //    if (id != parcel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            parcel.DateTimeJobLastEdit = DateTime.Now;
        //            parcel.ApplicationUserId = _userService.GetUserId();
        //            _context.Update(parcel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ParcelExists(parcel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(parcel);
        //}

        //// GET: Parcels/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var parcel = await _context.Parcel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (parcel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(parcel);
        //}

        //// POST: Parcels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var parcel = await _context.Parcel.FindAsync(id);
        //    _context.Parcel.Remove(parcel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ParcelExists(int id)
        //{
        //    return _context.Parcel.Any(e => e.Id == id);
        //}

        //public IActionResult ParcelInformationForm()
        //{
        //    return View();
        //}

    }
}
