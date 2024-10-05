using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WEB_253501_Rabets.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    [ValidateNever]
    [Display(Name = "Название")]
    public string Name { get; set; }

    [ValidateNever]
    public string NormalizedName { get; set; }
}
