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
        private readonly int _currentVisitorID;


        public SPUserModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAdminService adminService)
        {
            _context = context;
            _userManager = userManager;
            _adminService = adminService;
            _currentVisitorID = _adminService.GetAdminSPWarehouseId();
        }

        // GET: SPUserModels
        public async Task<IActionResult> Index()
        {

            var currentUserId = _adminService.GetAdminSPWarehouseId();
            var applicationDbContext = await _context.Users.ToListAsync();

            if (currentUserId == 999)
            {
                // if super user => show all user
                return View(applicationDbContext);
            }
            else
            {// else depending which warehouse they belong to, they only CURD the staff from their own warehouse
                return View(applicationDbContext
                         .Where(u => u.SPWarehouseModelIdIfUserIsAdmin == currentUserId));
            }

            return NotFound();
        }



        // GET: SPUserModels/Create
        public IActionResult Create()
        {

            if (_currentVisitorID == 999)

            {
                ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName");
            }
            else
            {
                var warehouse = _context.SPWarehouseModel.Where(w => w.Id == _currentVisitorID);

                ViewData["SPWarehouseModelId"] = new SelectList(warehouse, "Id", "CompanyName");
            }


            return View();
        }

        // POST: SPUserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,Role,ConfirmPassword,Email,SPWarehouseModelId,Role")] SPUserCreateAndLoginModel sPUserModel)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Role=sPUserModel.Role,
                    IsValidUser = true,
                    Email = sPUserModel.Email,
                    EmailConfirmed = true,
                    UserName = sPUserModel.Email,
                    SPWarehouseModelIdIfUserIsAdmin = sPUserModel.SPWarehouseModelId
                };
                var result = await _userManager.CreateAsync(user, sPUserModel.Password);
                var addRole =await  _adminService.AddSPUserToRole(user, sPUserModel.Role);
                return RedirectToAction(nameof(Index));
            }

            //if (_currentVisitorID == 999)
            //{
            //    ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName", sPUserModel.SPWarehouseModelId);

            //}
            if (_currentVisitorID == 999)

            {
                ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName");
            }
            else
            {
                var warehouse = _context.SPWarehouseModel.Where(w => w.Id == _currentVisitorID);

                ViewData["SPWarehouseModelId"] = new SelectList(warehouse, "Id", "CompanyName");
            }

            return View(sPUserModel);
        }

        // GET: SPUserModels/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName", sPUserModel.SPWarehouseModelId);
            if (_currentVisitorID == 999)

            {
                ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName");
            }
            else
            {
                var warehouse = _context.SPWarehouseModel.Where(w => w.Id == _currentVisitorID);

                ViewData["SPWarehouseModelId"] = new SelectList(warehouse, "Id", "CompanyName");
            }

            SPUserCreateAndLoginModel newUser = new SPUserCreateAndLoginModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                IsValid = user.IsValidUser,
                Role = user.Role,
                ApplicationUserId = user.Id

            };


            return View(newUser);
        }

        // POST: SPUserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Password,ConfirmPassword,ApplicationUserId,Email,SPWarehouseModelId,IsValid,Role")] SPUserCreateAndLoginModel sPUserModel)
        {


            if (id != sPUserModel.ApplicationUserId)
            {
                return NotFound();
            }
            var user = _userManager.Users.First(u => u.Id == sPUserModel.ApplicationUserId);

            if (user == null)
            {
                return View(sPUserModel);
            }
            // this is to ensure ModelState is valid

            if (sPUserModel.Password == null && sPUserModel.ConfirmPassword == null)
            {
                user.UserName = sPUserModel.UserName;
                user.Email = sPUserModel.Email;
                user.Role = sPUserModel.Role;
                _context.Update(user);
                _context.SaveChangesAsync().GetAwaiter().GetResult();

            }
            else if (ModelState.IsValid)
            {
                
                try
                {
                   var Password = sPUserModel.Password;

                    user.UserName = sPUserModel.UserName;
                    user.Email = sPUserModel.Email;
                    user.Role = sPUserModel.Role;
                    
                 var result =    _context.Update(user);
                    _context.SaveChangesAsync().GetAwaiter().GetResult();
                var finalResult =   await _adminService.ChangePasswordForSPAdmin(result.Entity,Password);



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

            if (_currentVisitorID == 999)

            {
                ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName");
            }
            else
            {
                var warehouse = _context.SPWarehouseModel.Where(w => w.Id == _currentVisitorID);

                ViewData["SPWarehouseModelId"] = new SelectList(warehouse, "Id", "CompanyName");
            }
            //ViewData["SPWarehouseModelId"] = new SelectList(_context.SPWarehouseModel, "Id", "CompanyName", sPUserModel.SPWarehouseModelId);
            return View(sPUserModel);
        }

        // GET: SPUserModels/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deleteUser == null)
            {
                return NotFound();
            }

            SPUserCreateAndLoginModel newUser = new SPUserCreateAndLoginModel()
            {
                UserName = deleteUser.UserName,
                Email = deleteUser.Email,
                IsValid = deleteUser.IsValidUser,
                Role = deleteUser.Role

            };

            return View(newUser);
        }

        // POST: SPUserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sPUserModel = await _context.Users.FindAsync(id);
            _context.Users.Remove(sPUserModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SPUserModelExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
