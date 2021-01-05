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
            var shippments = _context.Shippment.Where(spmts => spmts.ApplicationUserId == _currentUserId).Where(smpt => smpt.ModelIsvalid == true);
            var targetShippment = shippments.FirstOrDefault(spmt => spmt.Id == id);
            if (targetShippment == null)
            {
                return NotFound();
            }
            string SPtracking = targetShippment.SPTackingNumber;
            List<Parcel> parcels =  _context.Parcel.Where(parcel => parcel.SPTackingNumber == SPtracking).ToList();
            int AddressId = parcels[0].ConsumerAddressId;
            ConsumerAddress address = await _context.ConsumerAddress.FirstOrDefaultAsync(add => add.Id == AddressId);
            ViewBag.shippment = targetShippment;
            ViewBag.Parcels = parcels;
            ViewBag.Address = address;
            ViewBag.Count = parcels.Count;

            return View();
        }

        // POST: ConsumerShippments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            var form = Request.Form;


            if (form == null)
            {
                return NotFound();
            }


            string userId = _userService.GetUserId();
            int addressId = -1;


            var lookForAddress = _context.ConsumerAddress.FirstOrDefault(address => address
            .StreetAddress.ToLower().Replace(" ", "") == form["consumerAddress.StreetAddress"].ToString().ToLower().Replace(" ", "")); // =>to string => to lower => trim blank space

            if (lookForAddress == null)
            {
                //ConsumerAddress address = new ConsumerAddress()
                //{
                //    Country = form["consumerAddress.Country"].ToString(),
                //    StreetAddress = form["consumerAddress.StreetAddress"].ToString(),
                //    State = form["consumerAddress.State"].ToString(),
                //    Suburb = form["consumerAddress.Suburb"].ToString(),
                //    City = form["consumerAddress.City"].ToString(),
                //    PostCode = form["consumerAddress.PostCode"].ToString(),
                //    ApplicationUserId = userId
                //};

                //var addressResult = _context.ConsumerAddress.Add(address);
                //await _context.SaveChangesAsync();
                //addressId = addressResult.Entity.Id;
            }
            else
            {
                addressId = lookForAddress.Id;
            }

            string shippmentId = form["ShippmentId"].ToString();
            string SPNumber = form["SPTackingNumber"].ToString();
            // first generate a shippment so all parcels can go into that shippment

            if (true)      //ModelState.IsValid
            {
                var memebershipId = _userService.GetUserMemberId();
                for (int i = 0; i < form["OriginTrackingNumber"].Count; i++)
                {
                    var parcelCheckExisting = form["parcelId"].Count>i? form["parcelId"][i].ToString():null;
                    Parcel eachParcel;

                    if (parcelCheckExisting!="")
                    {

                        //eachParcel = new Parcel()
                        //{

                        //    DestinationAddressId = addressId,
                        //    ShippmentId = Int32.Parse(shippmentId),
                        //    ApplicationUserId = userId,
                        //    DestinationDeliverMethod = form["DestinationDeliverMethod"].ToString(),
                        //    CountryOfOrigin = form["CountryOfOrigin"].ToString(),
                        //    SPTackingNumber = SPNumber,
                        //    Id = Int32.Parse(parcelCheckExisting),
                        //    OriginCourierCompany = form["OriginCourierCompany"][i].ToString(),
                        //    OriginTrackingNumber = form["OriginTrackingNumber"][i].ToString(),
                        //    Description = form["Description"][i].ToString(),
                        //    EstimateWeight = form["EstimateWeight"][i].ToString(),
                        //    EstimateVolume = form["EstimateVolume"][i].ToString(),
                        //    TotalValue = form["TotalValue"][i].ToString(),
                        //    Reference = form["Reference"][i].ToString(),
                        //    NumberOfUnits = form["NumberOfUnits"][i].ToString(),

                        //};
                        //eachParcel.MemberShipId = memebershipId;
                    }
                    else
                    {
                        //eachParcel = new Parcel()
                        //{

                        //    DestinationAddressId = addressId,
                        //    ShippmentId = Int32.Parse(shippmentId),
                        //    ApplicationUserId = userId,
                        //    DestinationDeliverMethod = form["DestinationDeliverMethod"].ToString(),
                        //    CountryOfOrigin = form["CountryOfOrigin"].ToString(),
                        //    SPTackingNumber = SPNumber,
                        //    OriginCourierCompany = form["OriginCourierCompany"][i].ToString(),
                        //    OriginTrackingNumber = form["OriginTrackingNumber"][i].ToString(),
                        //    Description = form["Description"][i].ToString(),
                        //    EstimateWeight = form["EstimateWeight"][i].ToString(),
                        //    EstimateVolume = form["EstimateVolume"][i].ToString(),
                        //    TotalValue = form["TotalValue"][i].ToString(),
                        //    Reference = form["Reference"].Count>i? form["Reference"][i].ToString():"",
                        //    NumberOfUnits = form["NumberOfUnits"][i].ToString(),

                        //};
                        //eachParcel.MemberShipId = memebershipId;
                    }



                    //var findParcel = _context.Parcel.FirstOrDefault(parcel => parcel.Id == eachParcel.Id);

                    //if (findParcel == null)  //if new parcel => create
                    //{
                    //    _context.Parcel.Add(eachParcel);
                    //    await _context.SaveChangesAsync();
                    //}
                    //else if (CompareParcels(findParcel, eachParcel))
                    //{
                    //    // if parcel unchanged. continue
                    //}
                    //else  // if existing parcel and changes are made => update
                    //{
                    //    findParcel.OriginCourierCompany = form["OriginCourierCompany"][i].ToString();
                    //    findParcel.OriginTrackingNumber = form["OriginTrackingNumber"][i].ToString();
                    //    findParcel.Description = form["Description"][i].ToString();
                    //    findParcel.EstimateWeight = form["EstimateWeight"][i].ToString();
                    //    findParcel.EstimateVolume = form["EstimateVolume"][i].ToString();
                    //    findParcel.TotalValue = form["TotalValue"][i].ToString();
                    //    findParcel.Reference = form["Reference"][i].ToString();
                    //    findParcel.NumberOfUnits = form["NumberOfUnits"][i].ToString();
                    //    _context.Parcel.Update(findParcel);
                    //    await _context.SaveChangesAsync();
                    //}

                }

                ViewBag.Name = _userService.GetUserName();
                return RedirectToAction("Index", "ConsumerShippments");
            }



            ViewBag.Name = _userService.GetUserName();
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
            var parcels =  _context.Parcel.Where(parcel => parcel.ShippmentId == id);
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
