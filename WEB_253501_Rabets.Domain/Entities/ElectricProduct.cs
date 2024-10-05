using System.ComponentModel.DataAnnotations;

namespace WEB_253501_Rabets.Domain.Entities;

public class ElectricProduct
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Описание")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Категория")]
    public Category Category { get; set; }

    [Required]
    [Range(1, 1e10)]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Display(Name = "Путь к изображению")]
    public string? ImagePath { get; set; }
}
