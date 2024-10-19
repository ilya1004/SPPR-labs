namespace WEB_253501_Rabets.BlazorWasm.Entities;

public class ElectricProduct
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
}
