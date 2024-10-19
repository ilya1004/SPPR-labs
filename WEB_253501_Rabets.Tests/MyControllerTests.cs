using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Controllers;
using WEB_253501_Rabets.UI.Services.ApiCategoryService;
using WEB_253501_Rabets.UI.Services.ApiProductService;
using Xunit;

namespace WEB_253501_Rabets.Tests;

public class MyControllerTests
{
    private readonly ProductController _controller;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public MyControllerTests()
    {
        _productService = Substitute.For<IProductService>();
        _categoryService = Substitute.For<ICategoryService>();
        _controller = new ProductController(_productService, _categoryService);
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenCategoryListFails()
    {
        _productService.GetProductListAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(ResponseData<ProductListModel<ElectricProduct>>.Success(new ProductListModel<ElectricProduct>()));
        _categoryService.GetCategoryListAsync()
            .Returns(ResponseData<List<Category>>.Error("Error fetching categories"));

        var result = await _controller.Index("qweqwe", 1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Error fetching categories", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenProductListFails()
    {
        _productService.GetProductListAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(ResponseData<ProductListModel<ElectricProduct>>.Error("Error fetching products"));
        _categoryService.GetCategoryListAsync()
            .Returns(ResponseData<List<Category>>.Success(new List<Category>()));

        var result = await _controller.Index("all", 1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Error fetching products", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_Successful_ReturnsViewWithModelAndViewData()
    {
        var httpContext = Substitute.For<HttpContext>();

        var serviceProvider = Substitute.For<IServiceProvider>();

        var tempDataFactory = Substitute.For<ITempDataProvider>();
        serviceProvider.GetService(typeof(ITempDataProvider)).Returns(tempDataFactory);
        var tempDataDictionaryFactory = Substitute.For<ITempDataDictionaryFactory>();
        serviceProvider.GetService(typeof(ITempDataDictionaryFactory)).Returns(tempDataDictionaryFactory);
        
        httpContext.RequestServices.Returns(serviceProvider);

        var tempData = new TempDataDictionary(httpContext, tempDataFactory);
        httpContext.Items["TempData"] = tempData;

        var controllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        _controller.ControllerContext = controllerContext;

        var categories = new List<Category>
        {
            new Category { NormalizedName = "Category 1", Name = "Category 1 name" },
            new Category { NormalizedName = "Category 2", Name = "Category 2 name" },
        };
        var products = new ProductListModel<ElectricProduct> { 
            Items = [
                new ElectricProduct { Name = "Product 1", Category = categories[0], Description = "qwe", ImagePath = "qwe", Price = 123 }
                ]
        };

        _productService.GetProductListAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(ResponseData<ProductListModel<ElectricProduct>>.Success(products));
        _categoryService.GetCategoryListAsync()
            .Returns(ResponseData<List<Category>>.Success(categories));

        var result = await _controller.Index("all", 1);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(categories, viewResult.ViewData["categories"]);
        Assert.Equal("Все", viewResult.ViewData["currentCategory"]);
        Assert.Equal(products, viewResult.Model);
    }
}