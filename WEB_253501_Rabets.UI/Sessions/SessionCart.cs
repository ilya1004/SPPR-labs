using WEB_253501_Rabets.Domain.Models;
using System.Text.Json;
using WEB_253501_Rabets.Domain.Entities;

namespace WEB_253501_Rabets.UI.Sessions;

public class SessionCart : Cart
{
    private readonly IHttpContextAccessor _contextAccessor;
    public SessionCart(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
        LoadCartFromSession();
    }

    private void LoadCartFromSession()
    {
        var session = _contextAccessor.HttpContext.Session;
        var cartJson = session.GetString("CartSession");
        if (cartJson != null)
        {
            var items = JsonSerializer.Deserialize<Dictionary<int, CartItem>>(cartJson);
            if (items != null)
            {
                CartItems = items;
            }
        }
    }

    private void SaveCartToSession()
    {
        var session = _contextAccessor.HttpContext.Session;
        var cartJson = JsonSerializer.Serialize(CartItems);
        session.SetString("CartSession", cartJson);
    }

    public override void AddToCart(ElectricProduct product)
    {
        base.AddToCart(product);
        SaveCartToSession();
    }

    public override void RemoveItem(int id)
    {
        base.RemoveItem(id);
        SaveCartToSession();
    }

    public override void ClearAll()
    {
        base.ClearAll();
        SaveCartToSession();
    }
}