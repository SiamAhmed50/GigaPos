﻿using Microsoft.AspNetCore.Mvc;

namespace POS.UI.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
