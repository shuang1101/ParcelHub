﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelHub.Controllers
{
    public class SPParcelManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
