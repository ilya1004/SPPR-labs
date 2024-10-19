using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WEB_253501_Rabets.API.Data;
using WEB_253501_Rabets.API.Services.CategoryService;
using WEB_253501_Rabets.API.Services.ElectricProductService;
using WEB_253501_Rabets.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var authServer = builder.Configuration.GetSection("AuthServer").Get<AuthServerData>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
    { 
        options.MetadataAddress = $"{authServer.Host}/realms/{authServer.Realm}/.well-known/openid-configuration"; 
        options.Authority = $"{authServer.Host}/realms/{authServer.Realm}"; 
        options.Audience = "account";
        options.RequireHttpsMetadata = false; 
    });

builder.Services.AddAuthorizationBuilder().AddPolicy("admin", p => p.RequireRole("POWER-USER"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//await DbInitializer.SeedData(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
