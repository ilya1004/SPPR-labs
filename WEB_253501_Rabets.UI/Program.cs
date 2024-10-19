using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using Serilog.Events;
using WEB_253501_Rabets.API.Data;
using WEB_253501_Rabets.UI.Extensions;
using WEB_253501_Rabets.UI.Models;
using WEB_253501_Rabets.UI.Services.LoggingMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));

var uriData = builder.Configuration.GetSection("UriData").Get<UriData>();

builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri(uriData!.ApiUri);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddMvc();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.File("Logs/log.txt", LogEventLevel.Warning)
    .CreateLogger();

//var options = new ConfigurationReaderOptions { SectionName = "Serilog" };
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration, options)
//    .CreateLogger();

builder.Host.UseSerilog();

builder.RegisterCustomService();

var keycloakData = builder.Configuration.GetSection("Keycloak").Get<KeycloakData>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddJwtBearer()
    .AddOpenIdConnect(options =>
    {
        options.Authority = $"{keycloakData.Host}/auth/realms/{keycloakData.Realm}";
        options.ClientId = keycloakData.ClientId;
        options.ClientSecret = keycloakData.ClientSecret;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.Scope.Add("openid");

        options.SaveTokens = true;
        options.RequireHttpsMetadata = false;

        options.MetadataAddress = $"{keycloakData.Host}/realms/{keycloakData.Realm}/.well-known/openid-configuration";
    });

builder.Services.AddAuthorizationBuilder().AddPolicy("admin", p => p.RequireRole("POWER-USER"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages().RequireAuthorization("admin");

app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AreaAdmin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
