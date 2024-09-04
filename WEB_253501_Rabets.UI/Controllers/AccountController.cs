using Microsoft.AspNetCore.Mvc;

namespace WEB_253501_Rabets.UI.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult LogOut()
    {
        return RedirectToAction("Index", "Home");
    }
}
