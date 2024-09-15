using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253501_Rabets.API.Data;
using WEB_253501_Rabets.API.Services.CategoryService;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.API.Services.ElectricProductService;

public class ProductService : IProductService
{
    private readonly int _maxPageSize = 20;
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public Task<ResponseData<ElectricProduct>> CreateProductAsync(ElectricProduct product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<ElectricProduct>> GetProductByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
    {
        if (pageSize > _maxPageSize)
        {
            pageSize = _maxPageSize;
        }

        var query = _context.ElectricProducts.AsQueryable();

        var dataList = new ProductListModel<ElectricProduct>();

        categoryNormalizedName = categoryNormalizedName == "all" ? null : categoryNormalizedName;

        query = query.Where(p => categoryNormalizedName == null || p.Category.NormalizedName.Equals(categoryNormalizedName));

        var itemsCount = await query.CountAsync();

        if (itemsCount == 0)
        {
            return ResponseData<ProductListModel<ElectricProduct>>.Success(dataList);
        }

        var totalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);

        if (pageNo > totalPages)
        {
            return ResponseData<ProductListModel<ElectricProduct>>.Error("No such page");
        }

        dataList.Items = await query.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync();

        dataList.TotalPages = totalPages;
        dataList.CurrentPage = pageNo;

        return ResponseData<ProductListModel<ElectricProduct>>.Success(dataList);
    }

    public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(int id, ElectricProduct product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
