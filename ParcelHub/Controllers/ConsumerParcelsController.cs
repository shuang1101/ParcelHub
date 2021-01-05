﻿using System;
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
        public JsonResult GetWarehouseDetails([FromBody]string warehouseId)
        {
             int Id = Int32.Parse(warehouseId);
            try
            {
                var warehouse = _context.SPWarehouseModel.FirstOrDefault(warehouse => warehouse.Id == Id);

                return Json(warehouse);
            }
            catch
            {
                ArgumentNullException ex;
                return null;
            }  
        }




        public IActionResult SucceedPage()
        {
            return View();
        }



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
        public async Task<IActionResult> Create([Bind("Id,ShippmentId," +
            "SPTackingNumber,PackageLabelBarCode,ApplicationUserId," +
            "MemberShipId,OriginCourierCompany,OriginTrackingNumber," +
            "OriginSPWarehouseModelId,DestinatioSPWarehouseModelnId," +
            "ConsumerAddressId,Description,EstimateWeight,EstimateVolume," +
            "ActualVolume,ActualWeight,TotalValue,Reference,TransitStatus," +
            "DestinationDeliverMethod,NumberOfUnits,DateTimeInboundOrigin," +
            "DateTimeArriveInDestination,DateTimeJobCreated,DateTimeJobLastEdit," +
            "ModelIsvalid")] Parcel parcel)
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
                //ConsumerAddress address = new ConsumerAddress()
                //{
                //    CountryId = form["consumerAddress.Country"].ToString(),
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

            // first generate a shippment so all parcels can go into that shippment
            Shippment currentShippment = new Shippment()
            {
                ApplicationUserId = userId,
                Destination = form["consumerAddress.Country"].ToString(),
                Origin = form["CountryOfOrigin"].ToString(),
                
            };
            var memebershipId = _userService.GetUserMemberId();
            currentShippment.MemberShipId = memebershipId;
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
                    //Parcel eachParcel = new Parcel()
                    //{   
                    //    DestinationAddressId = addressId,
                    //    ShippmentId = shippmentId,
                    //    ApplicationUserId = userId,
                    //    DestinationDeliverMethod = form[$"DestinationDeliverMethod"].ToString(),
                    //    CountryOfOrigin = form["CountryOfOrigin"].ToString(),
                    //    SPTackingNumber=    SPNumber,
                    //    OriginCourierCompany = form[$"ShippingCompanyAtOrigin[{i}]"].ToString(),
                    //    OriginTrackingNumber = form[$"OriginTrackingNumber[{i}]"].ToString(),
                    //    Description = form[$"Description[{i}]"].ToString(),
                    //    EstimateWeight = form[$"EstimateWeight[{i}]"].ToString(),
                    //    EstimateVolume = form[$"EstimateVolume[{i}]"].ToString(),
                    //    TotalValue = form[$"TotalValue[{i}]"].ToString(),
                    //    Reference = form[$"Reference[{i}]"].ToString(),
                    //    NumberOfUnits = form[$"NumberOfUnits[{i}]"].ToString(),

                    //};
                    //eachParcel.MemberShipId = memebershipId;
                    //_context.Parcel.Add(eachParcel);
                    //await _context.SaveChangesAsync();
                }

                ViewBag.Name = _userService.GetUserName();
                return RedirectToAction("SucceedPage","ConsumerParcels");
            }



            ViewBag.Name = _userService.GetUserName();
            return View();
        }

     
    }
}
