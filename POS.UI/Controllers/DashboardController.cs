﻿using Microsoft.AspNetCore.Mvc;

namespace POS.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
