using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data.Common;
using System.Reflection.Metadata;
using WEB_253501_Rabets.API.Data;
using WEB_253501_Rabets.API.Services.ElectricProductService;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.Tests;

public class MyServiceTests
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<AppDbContext> _contextOptions;
    public MyServiceTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;

        using var context = new AppDbContext(_contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
                CREATE VIEW AllResources AS
                SELECT Name
                FROM Categories;";
            viewCommand.ExecuteNonQuery();
        }


        var categories = new List<Category>
        {
            new Category {Name = "Шуруповерты", NormalizedName = "screwdrivers"},
            new Category {Name = "Перфораторы", NormalizedName = "perforators"}
        };

        context.AddRange(categories);

        var url = "Images/";
        var products = new List<ElectricProduct>
        {
            new ElectricProduct {Name = "Шуруповерт Makita DF333DWYE", Description = "Профессиональная компактная и легкая дрель-шуруповерт",
                Price = 467.00M, ImagePath = url + "8026511.282413-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("screwdrivers"))! },
            new ElectricProduct {Name = "Дрель-шуруповерт Werker EWCDL 814", Description = "Аккумуляторная профессиональная дрель-шуруповерт",
                Price = 159.90M, ImagePath = url + "3404241.244685-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("screwdrivers"))! },
            new ElectricProduct {Name = "Перфоратор Makita HR 2470", Description = "Makita HR 2470 - профессиональный 3-режимный перфоратор.",
                Price = 603.25M, ImagePath = url + "6701911.149453-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("perforators"))! },
            new ElectricProduct {Name = "Перфоратор DEKO DKH850W", Description = "Перфоратор Deko DKH850W",
                Price = 176.55M, ImagePath = url + "4540811.370152-medium.jpg", Category = categories.Find(c => c.NormalizedName.Equals("perforators"))! }
        };

        context.AddRange(products);
        context.SaveChanges();
    }

    private AppDbContext CreateContext() => new AppDbContext(_contextOptions);

    private void Dispose() => _connection.Dispose();

    [Fact]
    public async Task ServiceReturnsFirstPageOfThreeItems()
    {
        using var context = CreateContext();
        var service = new ProductService(context);
        var result = await service.GetProductListAsync(null);
        Assert.IsType<ResponseData<ProductListModel<ElectricProduct>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(1, result.Data.CurrentPage);
        Assert.Equal(3, result.Data.Items.Count);
        Assert.Equal(2, result.Data.TotalPages);
        Assert.Equal(context.ElectricProducts.Include(c => c.Category).First().ToJson(), result.Data.Items[0].ToJson());
    }

    [Fact]
    public async Task ServiceReturnsCorrectSelectedPage()
    {
        using var context = CreateContext();
        var service = new ProductService(context);
        var result = await service.GetProductListAsync(null, 2);

        Assert.IsType<ResponseData<ProductListModel<ElectricProduct>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(2, result.Data.CurrentPage);
        Assert.Equal(1, result.Data.Items.Count);
        Assert.Equal(2, result.Data.TotalPages);
        Assert.Equal(context.ElectricProducts.Include(c => c.Category).FirstOrDefault(p => p.Id == 4).ToJson(), result.Data.Items[0].ToJson());
    }

    [Fact]
    public async Task ServiceReturnsCorrectProductsByCategory()
    {
        using var context = CreateContext();
        var service = new ProductService(context);
        var result = await service.GetProductListAsync("screwdrivers");

        Assert.IsType<ResponseData<ProductListModel<ElectricProduct>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(1, result.Data.CurrentPage);
        Assert.Equal(2, result.Data.Items.Count);
        Assert.Equal(1, result.Data.TotalPages);
        Assert.Equal(context.ElectricProducts.Include(c => c.Category).First().ToJson(), result.Data.Items[0].ToJson());
    }

    [Fact]
    public async Task ServiceReturnsCorrectHandingPageSizeValue()
    {
        using var context = CreateContext();
        var service = new ProductService(context);
        var result = await service.GetProductListAsync(null, 1, 10);

        Assert.IsType<ResponseData<ProductListModel<ElectricProduct>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(1, result.Data.CurrentPage);
        Assert.Equal(3, result.Data.Items.Count);
        Assert.Equal(2, result.Data.TotalPages);
        Assert.Equal(context.ElectricProducts.Include(c => c.Category).First().ToJson(), result.Data.Items[0].ToJson());
    }

    [Fact]
    public async Task ServiceReturnsCorrectHandingCurrentPageValue()
    {
        using var context = CreateContext();
        var service = new ProductService(context);
        var result = await service.GetProductListAsync(null, 10);

        Assert.IsType<ResponseData<ProductListModel<ElectricProduct>>>(result);
        Assert.False(result.Successfull);
    }
}
