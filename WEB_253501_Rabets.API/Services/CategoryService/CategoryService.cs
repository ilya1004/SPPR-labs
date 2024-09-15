using Microsoft.EntityFrameworkCore;
using WEB_253501_Rabets.API.Data;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.API.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    public CategoryService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = await _context.Categories.ToListAsync();

        return ResponseData<List<Category>>.Success(categories);
    }
}
