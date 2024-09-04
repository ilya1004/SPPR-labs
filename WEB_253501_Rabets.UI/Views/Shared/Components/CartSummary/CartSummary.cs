using Microsoft.AspNetCore.Mvc;

namespace WEB_253501_Rabets.UI.Views.Shared.Components.CartSummary;

public class CartSummary : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
