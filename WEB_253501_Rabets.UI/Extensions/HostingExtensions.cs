using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Models;
using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiFileService;
using WEB_253501_Rabets.UI.Services.ApiProductService;
using WEB_253501_Rabets.UI.Services.Authentication;
using WEB_253501_Rabets.UI.Services.Authorization;
using WEB_253501_Rabets.UI.Sessions;

namespace WEB_253501_Rabets.UI.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomService(this WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<Services.CategoryService.ICategoryService, MemoryCategoryService>();
        //builder.Services.AddScoped<Services.ElectricProductService.IProductService, MemoryProductService>();
        
        builder.Services.AddScoped<IProductService, ApiProductService>();
        builder.Services.AddScoped<ICategoryService, ApiCategoryService>();
        builder.Services.AddScoped<IFileService, ApiFileService>();
        builder.Services.AddScoped<ITokenAccessor, KeycloakTokenAccessor>();
        builder.Services.AddScoped<IAuthService, KeycloakAuthService>();

        builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));

        builder.Services.AddScoped<Cart, SessionCart>();
    }
}
