namespace WEB_253501_Rabets.Domain.Entities;

public class ElectricProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
}
