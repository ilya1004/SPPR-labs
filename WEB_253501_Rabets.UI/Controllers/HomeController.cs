using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WEB_253501_Rabets.UI.Models;

namespace WEB_253501_Rabets.UI.Controllers;

public class HomeController : Controller
{
    [ViewData]
    public string Text { get; set; } = "Лабораторная работа 2";
    public IActionResult Index()
    {
        var data = new List<ListDemo> { new() { Id = 1, Name = "Item 1"},
                                        new() { Id = 2, Name = "Item 2"},
                                        new() { Id = 3, Name = "Item 3"} };

        var list = new SelectList(data, nameof(ListDemo.Id), nameof(ListDemo.Name));

        

        return View(list);
    }

}
