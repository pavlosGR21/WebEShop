﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}