﻿using Microsoft.AspNetCore.Mvc;

namespace WEB_253501_Rabets.UI.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
