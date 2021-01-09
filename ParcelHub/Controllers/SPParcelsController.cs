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
    public class SPParcelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPParcelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPParcels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Parcel.Include(p => p.ApplicationUser).Include(p => p.ConsumerAddress);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SPParcels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcel
                .Include(p => p.ApplicationUser)
                .Include(p => p.ConsumerAddress)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // GET: SPParcels/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ConsumerAddressId"] = new SelectList(_context.ConsumerAddress, "Id", "Id");
            return View();
        }

        // POST: SPParcels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShippmentId,SPTackingNumber,PackageLabelBarCode,ApplicationUserId,MemberShipId,OriginCourierCompany,OriginTrackingNumber,TransportMethod,OriginSPWarehouseModelId,DestinatioSPWarehouseModelnId,ConsumerAddressId,Description,EstimateWeight,EstimateVolume,ActualVolume,ActualWeight,TotalValue,Reference,TransitStatus,DestinationDeliverMethod,RequireDelivery,NumberOfUnits,DateTimeInboundOrigin,DateTimeArriveInDestination,DateTimeJobCreated,DateTimeJobLastEdit,ModelIsvalid")] Parcel parcel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.ApplicationUserId);
            ViewData["ConsumerAddressId"] = new SelectList(_context.ConsumerAddress, "Id", "Id", parcel.ConsumerAddressId);
            return View(parcel);
        }

        // GET: SPParcels/Edit/5
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.ApplicationUserId);
            ViewData["ConsumerAddressId"] = new SelectList(_context.ConsumerAddress, "Id", "Id", parcel.ConsumerAddressId);
            return View(parcel);
        }

        // POST: SPParcels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShippmentId,SPTackingNumber,PackageLabelBarCode,ApplicationUserId,MemberShipId,OriginCourierCompany,OriginTrackingNumber,TransportMethod,OriginSPWarehouseModelId,DestinatioSPWarehouseModelnId,ConsumerAddressId,Description,EstimateWeight,EstimateVolume,ActualVolume,ActualWeight,TotalValue,Reference,TransitStatus,DestinationDeliverMethod,RequireDelivery,NumberOfUnits,DateTimeInboundOrigin,DateTimeArriveInDestination,DateTimeJobCreated,DateTimeJobLastEdit,ModelIsvalid")] Parcel parcel)
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.ApplicationUserId);
            ViewData["ConsumerAddressId"] = new SelectList(_context.ConsumerAddress, "Id", "Id", parcel.ConsumerAddressId);
            return View(parcel);
        }

        // GET: SPParcels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcel
                .Include(p => p.ApplicationUser)
                .Include(p => p.ConsumerAddress)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // POST: SPParcels/Delete/5
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
