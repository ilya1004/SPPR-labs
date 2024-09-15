using Microsoft.AspNetCore.Mvc;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiProductService;


namespace WEB_253501_Rabets.UI.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    public async Task<IActionResult> Index(string? category, int? page)
    {
        var productResponse = await _productService.GetProductListAsync(category, page.Value);
        var categories = await _categoryService.GetCategoryListAsync();

        if (!productResponse.Successfull)
        {
            return NotFound(productResponse.ErrorMessage);
        }

        ViewData["currentCategory"] = category != "all" ? categories.Data.Find(c => c.NormalizedName.Equals(category))?.Name : "Все";
        ViewData["categories"] = _categoryService.GetCategoryListAsync().Result.Data;
        ViewData["totalPages"] = productResponse.Data.TotalPages;

        return View(new ProductListModel<ElectricProduct> { Items = productResponse.Data!.Items, CurrentPage = 1, TotalPages = 1 });
    }

   
}
