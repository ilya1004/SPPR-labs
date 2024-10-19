using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WEB_253501_Rabets.BlazorWasm.Models;
using WEB_253501_Rabets.BlazorWasm.Services.Authentication;
using WEB_253501_Rabets.BlazorWasm.Services.Authorization;
using WEB_253501_Rabets.BlazorWasm.Services.DataService;

namespace WEB_253501_Rabets.BlazorWasm.Extensions;

public static class ServiceExtensions
{
    public static void RegisterCustomService(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IDataService, DataService>();
        builder.Services.AddScoped<ITokenAccessor, KeycloakTokenAccessor>();
        builder.Services.AddScoped<IAuthService, KeycloakAuthService>();

        builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));
    }
}
