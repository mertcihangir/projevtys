﻿using Microsoft.AspNetCore.Mvc;

namespace proje.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
