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
    public class ConsumerShippmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserSerivce _userService;
        private readonly UserManager<IdentityUser> _userManger;
        private readonly string _currentUserId;
        public ConsumerShippmentsController(ApplicationDbContext context,
            IUserSerivce userService)
        {
            _context = context;
            _userService = userService;
            _currentUserId = _userService.GetUserId();
        }

        // GET: ConsumerShippments
        public async Task<IActionResult> Index()
        {
            // 1. shippment under current consumer 2. shippment is markded valid
            var applicationDbContext = _context.Shippment
                .Where(spmt => spmt.ApplicationUserId == _currentUserId)
                .Where(spmt => spmt.ModelIsvalid == true);

            return View(await applicationDbContext.ToListAsync());
        }



        //GET: ConsumerShippments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var shippment = _context.Shippment
                .Where(spmts => spmts.ApplicationUserId == _currentUserId)
                .Where(smpt => smpt.ModelIsvalid == true)
                .FirstOrDefault(spmt => spmt.Id == id);

            if (shippment == null)
            {
                return NotFound();
            }
            string SPtracking = shippment.SPTackingNumber;

            ViewBag.Origin = _context.SPWarehouseModel.Find(shippment.OriginSPWarehouseModelId);
            ViewBag.Destination = _context.SPWarehouseModel.Find(shippment.DestinatioSPWarehouseModelnId);
            ViewBag.ConsumerAddress = _context.ConsumerAddress.Find(shippment.ConsumerAddressId);
           
            List<Parcel> parcels = _context.Parcel.Where(parcel => parcel.SPTackingNumber == SPtracking).ToList();
            ViewBag.Count = parcels.Count;
            ViewBag.RequireDelivery = shippment.RequireDelivery;
            ViewBag.Parcels = parcels;
            ViewBag.Shippment = shippment;
            return View();
        }

        // POST: ConsumerShippments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            var x = Request.Form;
                    
            
            return View();

        }

        // GET: ConsumerShippments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippment = await _context.Shippment.Where(spmt => spmt.ModelIsvalid == true)
                .Include(s => s.ApplicationUserId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippment == null)
            {
                return NotFound();
            }

            return View(shippment);
        }

        // POST: ConsumerShippments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippment = await _context.Shippment.FindAsync(id);
            var parcels = _context.Parcel.Where(parcel => parcel.ShippmentId == id);
            await parcels.ForEachAsync(p => p.ModelIsvalid = false);

            shippment.ModelIsvalid = false;



            _context.Shippment.Update(shippment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippmentExists(int id)
        {
            return _context.Shippment.Any(e => e.Id == id);
        }
        public bool CompareParcels(Parcel pA, Parcel pB)
        {
            return (pA.Description == pB.Description
                && pA.NumberOfUnits == pB.NumberOfUnits
                && pA.TotalValue == pB.TotalValue
                && pA.Reference == pB.Reference
                && pA.OriginCourierCompany == pB.OriginCourierCompany
                && pA.OriginTrackingNumber == pB.OriginTrackingNumber
                && pA.EstimateWeight == pB.EstimateWeight
                && pA.EstimateVolume == pB.EstimateVolume);
        }
    }
}
