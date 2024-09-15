using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiProductService;
using WEB_253501_Rabets.UI.Services.CategoryService;
using WEB_253501_Rabets.UI.Services.ElectricProductService;

namespace WEB_253501_Rabets.UI.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomService(this WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<Services.CategoryService.ICategoryService, MemoryCategoryService>();
        //builder.Services.AddScoped<Services.ElectricProductService.IProductService, MemoryProductService>();
        
        builder.Services.AddScoped<Services.ApiProductService.IProductService, ApiProductService>();
        builder.Services.AddScoped<Services.ApiCategoryService.ICategoryService, ApiCategoryService>();
    }
}
