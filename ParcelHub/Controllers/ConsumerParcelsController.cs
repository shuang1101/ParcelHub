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

        public ConsumerParcelsController(ApplicationDbContext context, IUserSerivce userService)//
        {
            _context = context;
            _userService = userService;
        }


        public IActionResult SucceedPage()
        {
            return View();
        }


        // GET: Parcels
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Parcel.Include(p => p.IdentityUser);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: Parcels/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var parcel = await _context.Parcel
        //        .Include(p => p.IdentityUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (parcel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(parcel);
        //}

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
        public async Task<IActionResult> Create(Object obj)
        {
            var form = Request.Form;


            if (Request == null)
            {
                return NotFound();
            }

            string userId = _userService.GetUserId();

            var lookForAddress = _context.ConsumerAddress.FirstOrDefault(address => address
            .StreetAddress.ToLower().Trim() == form["consumerAddress.StreetAddress"].ToString().ToLower().Trim()); // =>to string => to lower => trim blank space
            int addressId = -1;


            if (lookForAddress == null)
            {
                ConsumerAddress address = new ConsumerAddress()
                {
                    Country = form["consumerAddress.Country"].ToString(),
                    StreetAddress = form["consumerAddress.StreetAddress"].ToString(),
                    State = form["consumerAddress.State"].ToString(),
                    Suburb = form["consumerAddress.Suburb"].ToString(),
                    City = form["consumerAddress.City"].ToString(),
                    PostCode = form["consumerAddress.PostCode"].ToString(),
                    IdentityUserId = userId
                };

                var addressResult = _context.ConsumerAddress.Add(address);
                await _context.SaveChangesAsync();
                addressId = addressResult.Entity.Id;
              

            }
            else
            {
                addressId = lookForAddress.Id;
            }

            // first generate a shippment so all parcels can go into that shippment
            Shippment currentShippment = new Shippment()
            {
                IdentityUserId = userId,
                Destination = form["consumerAddress.Country"].ToString(),
                Origin = form["CountryOfOrigin"].ToString(),
                
            };
            
            var result = _context.Shippment.Add(currentShippment);

            await _context.SaveChangesAsync();
            int shippmentId = result.Entity.Id; //find the ID of above shippment just created

            string SPNumber ="KP"+DateTime.Now.ToString("yyyyMM")+shippmentId;
            currentShippment.SPTackingNumber = SPNumber;

            _context.Update(currentShippment);
            await _context.SaveChangesAsync();

            if (true)      //ModelState.IsValid
            {
                for (int i = 0; i < (form.Count - 7) / 8; i++)
                {
                    Parcel eachParcel = new Parcel()
                    {   
                        DestinationAddressId = addressId,
                        ShippmentId = shippmentId,
                        IdentityUserId = userId,
                        DestinationDeliverMethod = form[$"DestinationDeliverMethod"].ToString(),
                        CountryOfOrigin = form["CountryOfOrigin"].ToString(),
                        SPTackingNumber=    SPNumber,
                        OriginCourierCompany = form[$"ShippingCompanyAtOrigin[{i}]"].ToString(),
                        OriginTrackingNumber = form[$"OriginTrackingNumber[{i}]"].ToString(),
                        Description = form[$"Description[{i}]"].ToString(),
                        EstimateWeight = form[$"EstimateWeight[{i}]"].ToString(),
                        EstimateVolume = form[$"EstimateVolume[{i}]"].ToString(),
                        TotalValue = form[$"TotalValue[{i}]"].ToString(),
                        Reference = form[$"Reference[{i}]"].ToString(),
                        NumberOfUnits = form[$"NumberOfUnits[{i}]"].ToString(),

                    };
                    _context.Parcel.Add(eachParcel);
                    await _context.SaveChangesAsync();
                }

                ViewBag.Name = _userService.GetUserName();
                return RedirectToAction("SucceedPage","ConsumerParcels");
            }



            ViewBag.Name = _userService.GetUserName();
            return View();
        }

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
        //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.IdentityUserId);
        //    return View(parcel);
        //}

        //// POST: Parcels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ShippmentId,SPTackingNumber,PackageLabelBarCode,IdentityUserId,OriginTrackingNumber,Description,EstimateWeight,EstimateVolume,ActualVolume,ActualWeight,ItemValue,Reference,TransitStatus,DestinationDeliverMethod,Amount,Inbound,ArriveInDestination,JobCreated,JobLastEdit")] Parcel parcel)
        //{
        //    if (id != parcel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
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
        //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", parcel.IdentityUserId);
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
        //        .Include(p => p.IdentityUser)
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
    }
}
