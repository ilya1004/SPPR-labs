using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.UI.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cartJson = HttpContext.Session.GetString("CartSession");

        var cart = cartJson != null
            ? JsonSerializer.Deserialize<Cart>(cartJson)
            : new Cart();

        return View(cart);
    }
}