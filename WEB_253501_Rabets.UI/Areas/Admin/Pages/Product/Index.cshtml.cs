using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.UI.Services.ApiProductService;

namespace WEB_253501_Rabets.UI.Areas.Admin.Pages.Product;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public IList<ElectricProduct> ElectricProduct { get; set; } = default!;
    public async Task OnGetAsync()
    {
        var response = await _productService.GetProductListAsync("all", 0);
        ElectricProduct = response.Data.Items;
    }
}
