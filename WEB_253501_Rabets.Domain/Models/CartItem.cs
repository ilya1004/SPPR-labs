using WEB_253501_Rabets.Domain.Entities;

namespace WEB_253501_Rabets.Domain.Models;

public class CartItem
{
    public ElectricProduct Product { get; set; }
    public int Count { get; set; } = 0;
}
