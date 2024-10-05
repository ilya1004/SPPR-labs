using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiProductService;

namespace WEB_253501_Rabets.UI.Areas.Admin.Pages.Product;

public class CreateModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public CreateModel(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> OnGet()
    {
        var responseData = await _categoryService.GetCategoryListAsync();
        Categories = responseData.Data;
        CategoryItems = Categories.Select(c => new SelectListItem()
        {
            Value = c.Id.ToString(),
            Text = c.Name,
            Selected = false,
        });
        return Page();
    }

    [BindProperty]
    public ElectricProduct ElectricProduct { get; set; } = default!;

    [BindProperty]
    public IFormFile? Image { get; set; }

    [BindProperty]
    public IEnumerable<Category> Categories { get; set; }

    [BindProperty]
    public IEnumerable<SelectListItem> CategoryItems { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Пожалуйста, исправьте ошибки в форме.");
            return Page();
        }

        await _productService.CreateProductAsync(ElectricProduct, Image);

        return RedirectToPage("./Index");
    }
}
