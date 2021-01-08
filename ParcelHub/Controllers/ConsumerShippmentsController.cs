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
            var shippments = _context.Shippment
                .Where(spmt => spmt.ApplicationUserId == _currentUserId)
                .Where(spmt => spmt.ModelIsvalid == true);

            return View(await shippments.ToListAsync());
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
            // read from form XML
            var form = Request.Form;

            var applicationUserId = _userService.GetUserId();
            var memberShipId = _userService.GetUserMemberId();
            var SPTracking = form["SPTackingNumber"].ToString();
            var shippmentId = Int32.Parse(form["ShippmentId"].ToString());
            var shippment = _context.Shippment.FirstOrDefault(s => s.Id == shippmentId);


            var consumerAddressId = shippment.ConsumerAddressId;
            var originSPWarehouseModelId = shippment.OriginSPWarehouseModelId;
            var destinatioSPWarehouseModelnId = shippment.DestinatioSPWarehouseModelnId;
            var transportModel = shippment.TransportMethod;

            // for new item, the parcelId is -1, this is set in mysite.js
            var parcelId = form["parcelId"].ToList();
            var originCourierCompany = form["OriginCourierCompany"].ToList();
            var originTrackingNumber = form["OriginTrackingNumber"].ToList();
            var description = form["Description"].ToList();
            var estimateWeight = form["EstimateWeight"].ToList();
            var estimateVolume = form["EstimateVolume"].ToList();
            var numberOfUnits = form["NumberOfUnits"].ToList();
            var totalValue = form["TotalValue"].ToList();
            var reference = form["Reference"].ToList();

            var now = DateTime.Now;

            // deliveryMethod read from HTML for changes
            var deliveryMethod = form["DestinationDeliverMethod"].ToString();

            bool requireDelivery = false;

            if (deliveryMethod == "DelivertoDoor")
            {
                requireDelivery = true;
            }

            List<int> IDOfParcelThatAreNotDeleted = new List<int>();
            List<Parcel> parcelsToAdd = new List<Parcel>();

            var parcels = await _context.Parcel.Where(p => p.SPTackingNumber == SPTracking).ToListAsync();


            for (int i = 0; i < parcelId.Count; i++)
            {

                var p_Id = Int32.Parse(parcelId[i].ToString());

                if (p_Id == -1)
                // if p_Id == -1 that means it is a new entry which was not in system before => need to add new parcel
                // the -1 is set in mysite.js file. 
                {


                    Parcel newParcel = new Parcel()
                    {
                        MemberShipId = memberShipId,
                        ConsumerAddressId = consumerAddressId,
                        ShippmentId = shippmentId,
                        ApplicationUserId = applicationUserId,
                        DestinationDeliverMethod = deliveryMethod,
                        DestinatioSPWarehouseModelnId = destinatioSPWarehouseModelnId,
                        OriginSPWarehouseModelId = originSPWarehouseModelId,
                        SPTackingNumber = SPTracking,
                        OriginCourierCompany = originCourierCompany[i].ToString(),
                        OriginTrackingNumber = originTrackingNumber[i].ToString(),
                        Description = description[i].ToString(),
                        EstimateWeight = estimateWeight[i].ToString(),
                        EstimateVolume = estimateVolume[i].ToString(),
                        TotalValue = totalValue[i].ToString(),
                        Reference = reference[i].ToString(),
                        NumberOfUnits = numberOfUnits[i].ToString(),
                        DateTimeJobLastEdit = now,
                        RequireDelivery = requireDelivery,
                        TransportMethod = transportModel
                    };
                    parcelsToAdd.Add(newParcel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var p = parcels.FirstOrDefault(p => p.Id == p_Id);
                    IDOfParcelThatAreNotDeleted.Add(p_Id);
                    p.OriginCourierCompany = originCourierCompany[i].ToString();
                    p.OriginTrackingNumber = originTrackingNumber[i].ToString();
                    p.Description = description[i].ToString();
                    p.EstimateWeight = estimateWeight[i].ToString();
                    p.EstimateVolume = estimateVolume[i].ToString();
                    p.TotalValue = totalValue[i].ToString();
                    p.Reference = reference[i].ToString();
                    p.NumberOfUnits = numberOfUnits[i].ToString();
                    p.DateTimeJobLastEdit = now;
                    p.RequireDelivery = requireDelivery;
                    p.DestinationDeliverMethod = deliveryMethod;

                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
            }



            // delete parcels that has been deleted // this is done by setting ModelIsvalid to false
            foreach (var p in parcels)
            {
                if (!IDOfParcelThatAreNotDeleted.Contains(p.Id))
                {
                    p.ModelIsvalid = false;
                    _context.Parcel.Update(p);
                    await _context.SaveChangesAsync();
                }
            }
            foreach (var parcel in parcelsToAdd)
            {
                _context.Parcel.Add(parcel);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index", "ConsumerShippments");



        }


        public IActionResult Succeed()
        {
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

    }
}

