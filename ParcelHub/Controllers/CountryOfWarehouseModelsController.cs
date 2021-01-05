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
    public class CountryOfWarehouseModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryOfWarehouseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CountryOfWarehouseModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CountryOfWarehouseModel.ToListAsync());
        }

        // GET: CountryOfWarehouseModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryOfWarehouseModel = await _context.CountryOfWarehouseModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryOfWarehouseModel == null)
            {
                return NotFound();
            }

            return View(countryOfWarehouseModel);
        }

        // GET: CountryOfWarehouseModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountryOfWarehouseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryName,Id")] CountryOfWarehouseModel countryOfWarehouseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countryOfWarehouseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryOfWarehouseModel);
        }

        // GET: CountryOfWarehouseModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryOfWarehouseModel = await _context.CountryOfWarehouseModel.FindAsync(id);
            if (countryOfWarehouseModel == null)
            {
                return NotFound();
            }
            return View(countryOfWarehouseModel);
        }

        // POST: CountryOfWarehouseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryName,Id")] CountryOfWarehouseModel countryOfWarehouseModel)
        {
            if (id != countryOfWarehouseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryOfWarehouseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryOfWarehouseModelExists(countryOfWarehouseModel.Id))
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
            return View(countryOfWarehouseModel);
        }

        // GET: CountryOfWarehouseModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryOfWarehouseModel = await _context.CountryOfWarehouseModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countryOfWarehouseModel == null)
            {
                return NotFound();
            }

            return View(countryOfWarehouseModel);
        }

        // POST: CountryOfWarehouseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countryOfWarehouseModel = await _context.CountryOfWarehouseModel.FindAsync(id);
            _context.CountryOfWarehouseModel.Remove(countryOfWarehouseModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryOfWarehouseModelExists(int id)
        {
            return _context.CountryOfWarehouseModel.Any(e => e.Id == id);
        }
    }
}
