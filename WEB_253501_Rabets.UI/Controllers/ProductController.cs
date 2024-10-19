using Microsoft.AspNetCore.Mvc;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiProductService;


namespace WEB_253501_Rabets.UI.Controllers;

[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet("{category}/{page}")]
    public async Task<IActionResult> Index(string category, int? page)
    {
        var productResponse = await _productService.GetProductListAsync(category, page!.Value);

        if (!productResponse.Successfull)
        {
            return NotFound(productResponse.ErrorMessage);
        }

        var categoryResponse = await _categoryService.GetCategoryListAsync();

        if (!categoryResponse.Successfull)
        {
            return NotFound(categoryResponse.ErrorMessage);
        }

        ViewData["currentCategory"] = category != "all" ? categoryResponse.Data.Find(c => c.NormalizedName.Equals(category))?.Name : "Все";
        ViewData["categories"] = _categoryService.GetCategoryListAsync().Result.Data;
        ViewData["totalPages"] = productResponse.Data.TotalPages;

        //if (Request.Headers.XRequestedWith.ToString().ToLower().Equals("xmlhttprequest"))
        //{
        //    return PartialView("_ProductsListPartial", productResponse.Data);
        //}

        if (Request.Headers["X-requested-with"].ToString().Equals("XMLHttpRequest"))
        {
            return PartialView("_ProductsListPartial", productResponse.Data);
        }

        return View(productResponse.Data);
    }
  
}
