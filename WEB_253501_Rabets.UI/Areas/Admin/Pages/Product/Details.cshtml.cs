using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.UI.Services.ApiProductService;

namespace WEB_253501_Rabets.UI.Areas.Admin.Pages.Product;

public class DetailsModel : PageModel
{
    private readonly IProductService _productService;
    public DetailsModel(IProductService productService)
    {
       _productService = productService;
    }

    public ElectricProduct ElectricProduct { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var responseData = await _productService.GetProductByIdAsync(id!.Value);
        if (!responseData.Successfull)
        {
            return NotFound();
        }
        else
        {
            ElectricProduct = responseData.Data!;
        }
        return Page();
    }
}
