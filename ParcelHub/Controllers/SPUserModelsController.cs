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
    public class SPUserModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;

        public SPUserModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAdminService adminService)
        {
            _context = context;
            _userManager = userManager;
            _adminService = adminService;
        }

        // GET: SPUserModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SPUserModel.Include(s => s.SPWarehouseModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SPUserModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPUserModel = await _context.SPUserModel
                .Include(s => s.SPWarehouseModel)
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            if (sPUserModel == null)
            {
                return NotFound();
            }

            return View(sPUserModel);
        }

        // GET: SPUserModels/Create
        public IActionResult Create()
        {
            if (_adminService.GetAdminSPWarehouseId() == 999)

            {
                ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName");
            }


            return View();
        }

        // POST: SPUserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,ConfirmPassword,ApplicationUserId,Email,SPWarehouseModelId,Role")] SPUserModel sPUserModel)
        {
            sPUserModel.IsValid = true;
            
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = sPUserModel.Email,
                    EmailConfirmed = true,
                    UserName = sPUserModel.Email,
                    SPWarehouseModelIdIfUserIsAdmin =sPUserModel.SPWarehouseModelId
                };
                var result = await _userManager.CreateAsync(user, sPUserModel.Password);


                if (result.Succeeded)
                {
                    sPUserModel.Password = "hide";
                    sPUserModel.ConfirmPassword = "hide";
                    _context.Add(sPUserModel);
                    await _context.SaveChangesAsync();
                }



                // pass userModel + password => save in DB

                return RedirectToAction(nameof(Index));
            }
            if (_adminService.GetAdminSPWarehouseId() == 999)
            {
                ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName", sPUserModel.SPWarehouseModelId);

            }

            return View(sPUserModel);
        }

        // GET: SPUserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPUserModel = await _context.SPUserModel.FindAsync(id);
            if (sPUserModel == null)
            {
                return NotFound();
            }
            ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName", sPUserModel.SPWarehouseModelId);
            return View(sPUserModel);
        }

        // POST: SPUserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,ApplicationUserId,Email,SPWarehouseModelId,IsValid,Role")] SPUserModel sPUserModel)
        {
            if (id != sPUserModel.ApplicationUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sPUserModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SPUserModelExists(sPUserModel.ApplicationUserId))
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
            ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName", sPUserModel.SPWarehouseModelId);
            return View(sPUserModel);
        }

        // GET: SPUserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sPUserModel = await _context.SPUserModel
                .Include(s => s.SPWarehouseModel)
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            if (sPUserModel == null)
            {
                return NotFound();
            }

            return View(sPUserModel);
        }

        // POST: SPUserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sPUserModel = await _context.SPUserModel.FindAsync(id);
            _context.SPUserModel.Remove(sPUserModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPUserModelExists(int id)
        {
            return _context.SPUserModel.Any(e => e.ApplicationUserId == id);
        }
    }
}
