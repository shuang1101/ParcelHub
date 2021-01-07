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
        [Route("/data/getWarehouseDetails")]
        [HttpPost]
        public JsonResult GetWarehouseDetails([FromBody] string warehouseId)
        {
            if (warehouseId == null)
            {
                return null;
            }

            try
            {
                int Id = Int32.Parse(warehouseId);
                var warehouse = _context.SPWarehouseModel.FirstOrDefault(warehouse => warehouse.Id == Id);

                return Json(warehouse);
            }
            catch
            {
                ArgumentNullException ex;
                return null;
            }
        }
        [Route("/data/getReceiverAddressId")]
        [HttpPost]
        public JsonResult GetConsumerAddress([FromBody] string warehouseId)
        {
            try
            {

                var address = _context.ConsumerAddress.Find(Int32.Parse(warehouseId));
                string countryName = _context.CountryOfWarehouseModel.Find(address.CountryOfWarehouseModelIdAtDestination).CountryName;

                var json = new
                {
                    ReceiverName = address.NameOfReceiver,
                    Country = countryName,
                    StreetAddress = address.StreetAddress,
                    Suburb = address.Suburb,
                    City = address.City,
                    State = address.State,
                    PostCode = address.PostCode
                };

                return Json(json);
            }
            catch
            {
                ArgumentNullException ex;
                return null;
            }

        }


        public IActionResult SucceedPage(SPWarehouseModel? warehouse)
        {
            return View(warehouse);
        }



        // GET: Parcels/Create
        public IActionResult Create()
        {
            // ViewBag.Name = _userService.GetUserName();
            ViewBag.Name = _userService.GetUserName();
            var consumerAddress = _context.ConsumerAddress.Where(address => address.ApplicationUserId == _userService.GetUserId());

            ViewBag.NoAddress = false;
            if (consumerAddress == null)
            {
                ViewBag.NoAddress = true;
            }
            else
            {

                ViewBag.ListOfAddress = consumerAddress;
            }


            return View();
        }

        // POST: Parcels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id," +
            "OriginSPWarehouseModelId,DestinatioSPWarehouseModelnId," +
            "ConsumerAddressId," +
            "DestinationDeliverMethod" )] Parcel parcel)
        {
            var form = Request.Form;

            var applicationUserId = _userService.GetUserId();
            var memberShipId = _userService.GetUserMemberId();
            var destinatioSPWarehouseModelnId = 100;
            var now = DateTime.Now;
            var consumerAddressId = parcel.ConsumerAddressId;
            var deliveryMethod = parcel.DestinationDeliverMethod;
            var originSPWarehouseModelId = parcel.OriginSPWarehouseModelId;
            bool requireDelivery = false;
            if (parcel.DestinationDeliverMethod == "DelivertoDoor")
            {
                requireDelivery = true; 
            }

            // first generate a shippment so all parcels can go into that shippment
            Shippment currentShippment = new Shippment()
            {
                ApplicationUserId = applicationUserId,
                OriginSPWarehouseModelId = originSPWarehouseModelId,
                DestinatioSPWarehouseModelnId = destinatioSPWarehouseModelnId,
                ConsumerAddressId = consumerAddressId,
                MemberShipId = memberShipId,
                RequireDelivery=requireDelivery
            };
            var shipEntity = _context.Add(currentShippment);
            _context.SaveChangesAsync().GetAwaiter().GetResult();

            //find the ID of above shippment just created
            var shippmentId = shipEntity.Entity.Id;

            // Generate SPNumber for tracking and update this shippment
            string SPNumber = "KP" + DateTime.Now.ToString("yyyyMM") + shippmentId;
            currentShippment.SPTackingNumber = SPNumber;

            _context.Update(currentShippment);
            await _context.SaveChangesAsync();


            for (int i = 0; i < (form.Count) / 8; i++)
            {
                parcel = new Parcel()
                {
                    MemberShipId = memberShipId,
                    ConsumerAddressId = consumerAddressId,
                    ShippmentId = shippmentId,
                    ApplicationUserId = applicationUserId,
                    DestinationDeliverMethod = deliveryMethod,
                    DestinatioSPWarehouseModelnId = destinatioSPWarehouseModelnId,
                    OriginSPWarehouseModelId = originSPWarehouseModelId,
                    SPTackingNumber = SPNumber,
                    OriginCourierCompany = form[$"ShippingCompanyAtOrigin[{i}]"].ToString(),
                    OriginTrackingNumber = form[$"OriginTrackingNumber[{i}]"].ToString(),
                    Description = form[$"Description[{i}]"].ToString(),
                    EstimateWeight = form[$"EstimateWeight[{i}]"].ToString(),
                    EstimateVolume = form[$"EstimateVolume[{i}]"].ToString(),
                    TotalValue = form[$"TotalValue[{i}]"].ToString(),
                    Reference = form[$"Reference[{i}]"].ToString(),
                    NumberOfUnits = form[$"NumberOfUnits[{i}]"].ToString(),
                    DateTimeJobLastEdit=now,
                    RequireDelivery=requireDelivery

                };

                    _context.Parcel.Add(parcel);
                    await _context.SaveChangesAsync();
               
            }
            SPWarehouseModel origin = _context.SPWarehouseModel.FirstOrDefault(warehouse=>warehouse.Id==originSPWarehouseModelId);
            
            return RedirectToAction("SucceedPage", "ConsumerParcels",origin);
        }
    }
}