using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Extensions;
using WEB_253501_Rabets.UI.Services.ApiProductService;

namespace WEB_253501_Rabets.UI.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly Cart _cart;
    public CartController(IProductService productService, Cart cart)
    {
        _productService = productService;
        _cart = cart;
    }

    public IActionResult Index()
    {
        return View(_cart);
    }

    [Authorize]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Add(int id, string returnUrl)
    {
        var data = await _productService.GetProductByIdAsync(id);
        if (data.Successfull)
        {
            _cart.AddToCart(data.Data);
        }
        return Redirect(returnUrl);
    }

    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> ClearCart(string returnUrl)
    {
        _cart.ClearAll();
        return Redirect(returnUrl);
    }

    [Authorize]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> RemoveFromCart(int id, string returnUrl)
    {
        _cart.RemoveItem(id);
        return Redirect(returnUrl);
    }
}
