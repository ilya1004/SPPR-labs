using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.UI.Services.ApiProductService;

namespace WEB_253501_Rabets.UI.Areas.Admin.Pages.Product;

public class DeleteModel : PageModel
{
    private readonly IProductService _productService;
    public DeleteModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ElectricProduct ElectricProduct { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var responseData = await _productService.GetProductByIdAsync(id.Value);
        var electricproduct = responseData.Data!;

        if (electricproduct == null)
        {
            return NotFound();
        }
        else
        {
            ElectricProduct = electricproduct;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var responseData = await _productService.DeleteProductAsync(id.Value);
        if (!responseData.Successfull)
        {
            return RedirectToPage("./Index");
        }

        return RedirectToPage("./Index");
    }
}
