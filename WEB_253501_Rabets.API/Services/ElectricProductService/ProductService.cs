using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WEB_253501_Rabets.API.Data;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.API.Services.ElectricProductService;

public class ProductService : IProductService
{
    private readonly int _maxPageSize = 3;
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseData<int>> CreateProductAsync(ElectricProduct product)
    {
        if ((product.Category.Name.IsNullOrEmpty() || product.Category.Name.IsNullOrEmpty()) && product.Category != null)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == product.Category.Id);
            product.Category = category;
        }
        var item = await _context.ElectricProducts.AddAsync(product);
        await _context.SaveChangesAsync();
        return ResponseData<int>.Success(item.Entity.Id);
    }

    public async Task<ResponseData<bool>> DeleteProductAsync(int id)
    {
        if (id < 0)
        {
            return ResponseData<bool>.Error("No such id");
        }

        await _context.ElectricProducts.Where(p => p.Id == id).ExecuteDeleteAsync();
        return ResponseData<bool>.Success(true);
    }

    public async Task<ResponseData<ElectricProduct>> GetProductByIdAsync(int id)
    {
        if (id < 0)
        {
            return ResponseData<ElectricProduct>.Error("No such id");
        }

        var dataItem = await _context.ElectricProducts.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        return ResponseData<ElectricProduct>.Success(dataItem!);
    }

    public async Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
    {
        if (pageSize > _maxPageSize)
        {
            pageSize = _maxPageSize;
        }

        var query = _context.ElectricProducts.AsQueryable().Include(p => p.Category).AsNoTracking();

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

        if (pageNo == 0)
        {
            dataList.Items = await query.ToListAsync();
        }
        else
        {
            dataList.Items = await query.OrderBy(p => p.Id).Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync();
        }

        dataList.TotalPages = totalPages;
        dataList.CurrentPage = pageNo;

        return ResponseData<ProductListModel<ElectricProduct>>.Success(dataList);
    }

    public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseData<bool>> UpdateProductAsync(int id, ElectricProduct product)
    {
        if (id < 0)
        {
            return ResponseData<bool>.Error("No such id");
        }

        //if ((product.Category.Name.IsNullOrEmpty() || product.Category.Name.IsNullOrEmpty()) && product.Category != null)
        //{
        //    var category = _context.Categories.FirstOrDefault(c => c.Id == product.Category.Id);
        //    product.Category = category;
        //}

        if (product.ImagePath == null)
        {
            var item = _context.ElectricProducts.FirstOrDefault(p => p.Id == id);
            product.ImagePath = item.ImagePath;
        }

        var existingProduct = await _context.ElectricProducts.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.ImagePath = product.ImagePath;
            existingProduct.Category = await _context.Categories.FindAsync(product.Category.Id); // Обновите категорию по ID

            await _context.SaveChangesAsync();
        }


        return ResponseData<bool>.Success(true);
    }
}
