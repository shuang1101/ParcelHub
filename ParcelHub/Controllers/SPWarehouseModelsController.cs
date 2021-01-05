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
    public class SPWarehouseModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SPWarehouseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SPWarehouseModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.SPWarehouseModel.ToListAsync());
        }

        // GET: SPWarehouseModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPWarehouseModel = await _context.SPWarehouseModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPWarehouseModel == null)
            {
                return NotFound();
            }

            return View(sPWarehouseModel);
        }

        // GET: SPWarehouseModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SPWarehouseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,ContactName,Email,ModelIsvalid,Mobile,AddressLine1,AddressLine2,AddressLine3,CountryId,City,PostCode,ReceiverName,AirService,LandService,OcreanFreightService")] SPWarehouseModel sPWarehouseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sPWarehouseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sPWarehouseModel);
        }

        // GET: SPWarehouseModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPWarehouseModel = await _context.SPWarehouseModel.FindAsync(id);
            if (sPWarehouseModel == null)
            {
                return NotFound();
            }
            return View(sPWarehouseModel);
        }

        // POST: SPWarehouseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ContactName,Email,ModelIsvalid,Mobile,AddressLine1,AddressLine2,AddressLine3,CountryId,City,PostCode,ReceiverName,AirService,LandService,OcreanFreightService")] SPWarehouseModel sPWarehouseModel)
        {
            if (id != sPWarehouseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPWarehouseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPWarehouseModelExists(sPWarehouseModel.Id))
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
            return View(sPWarehouseModel);
        }

        // GET: SPWarehouseModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPWarehouseModel = await _context.SPWarehouseModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sPWarehouseModel == null)
            {
                return NotFound();
            }

            return View(sPWarehouseModel);
        }

        // POST: SPWarehouseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sPWarehouseModel = await _context.SPWarehouseModel.FindAsync(id);
            _context.SPWarehouseModel.Remove(sPWarehouseModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPWarehouseModelExists(int id)
        {
            return _context.SPWarehouseModel.Any(e => e.Id == id);
        }
    }
}
