using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiProductService;

namespace WEB_253501_Rabets.UI.Areas.Admin.Pages.Product;

public class EditModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public EditModel(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [BindProperty]
    public ElectricProduct ElectricProduct { get; set; } = default!;

    [BindProperty]
    public IEnumerable<Category> Categories { get; set; }

    [BindProperty]
    public IFormFile? Image { get; set; }

    [BindProperty]
    public IEnumerable<SelectListItem> CategoryItems { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var responseData = await _productService.GetProductByIdAsync(id.Value!);
        var electricproduct = responseData.Data;
        if (electricproduct == null)
        {
            return NotFound();
        }
        ElectricProduct = electricproduct;

        var responseData1 = await _categoryService.GetCategoryListAsync();
        Categories = responseData1.Data;
        CategoryItems = Categories.Select(c => new SelectListItem()
        {
            Value = c.Id.ToString(),
            Text = c.Name,
            Selected = c.Id == ElectricProduct.Category.Id ? true : false,
        });

        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    { 
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Пожалуйста, исправьте ошибки в форме.");
            return Page();
        }

        try
        {
            var responseData = await _productService.UpdateProductAsync(ElectricProduct.Id, ElectricProduct, Image);
        }
        catch
        {
            return Page();
        }

        return RedirectToPage("./Index");
    }

}
