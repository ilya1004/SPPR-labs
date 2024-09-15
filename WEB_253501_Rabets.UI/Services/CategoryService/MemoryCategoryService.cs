using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.UI.Services.CategoryService;

public class MemoryCategoryService : ICategoryService
{
    public Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = new List<Category>
        {
            new Category {Id = 1, Name = "Шуруповерты", NormalizedName = "screwdrivers"},
            new Category {Id = 2, Name = "Перфораторы", NormalizedName = "perforators"},
            new Category {Id = 3, Name = "Циркулярные пилы", NormalizedName = "circular_saws"},
            new Category {Id = 4, Name = "Дрели", NormalizedName = "drills"},
            new Category {Id = 5, Name = "Гайковерты", NormalizedName = "wrenches"},
        };
        
        var res = ResponseData<List<Category>>.Success(categories);

        return Task.FromResult(res);
    }
}
