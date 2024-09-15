using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253501_Rabets.API.Services.CategoryService;
using WEB_253501_Rabets.Domain.Entities;

namespace WEB_253501_Rabets.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IQueryable<Category>>> GetCategories()
    {
        var serviceResponse = await _categoryService.GetCategoryListAsync();
        return new ActionResult<IQueryable<Category>>(serviceResponse.Data.AsQueryable());
    }

}
