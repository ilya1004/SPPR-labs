using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253501_Rabets.API.Services.ElectricProductService;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;

namespace WEB_253501_Rabets.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ElectricProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ElectricProductsController(IProductService electricProductService)
    {
        _productService = electricProductService;
    }

    // GET: api/ElectricProducts/category
    [HttpGet]
    [Route("{category}")]
    public async Task<ActionResult<ResponseData<ProductListModel<ElectricProduct>>>> GetElectricProducts(string? category, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 3)
    {
        var serviceResponse = await _productService.GetProductListAsync(category, pageNo, pageSize);
        return new ActionResult<ResponseData<ProductListModel<ElectricProduct>>>(serviceResponse);
    }

    // GET: api/ElectricProducts/5
    [HttpGet("{id:int}")]
    [Authorize("admin")]
    public async Task<ActionResult<ResponseData<ElectricProduct>>> GetElectricProduct(int id)
    {
        var serviceResponse = await _productService.GetProductByIdAsync(id);

        if (!serviceResponse.Successfull)
        {
            return NotFound();
        }

        return new ActionResult<ResponseData<ElectricProduct>>(serviceResponse);
    }

    // PUT: api/ElectricProducts/5
    [HttpPut("{id:int}")]
    [Authorize("admin")]
    public async Task<ActionResult<ResponseData<bool>>> PutElectricProduct(int id, ElectricProduct electricProduct)
    {
        var serviceResponse = await _productService.UpdateProductAsync(id, electricProduct);

        if (!serviceResponse.Successfull)
        {
            return NotFound();
        }

        return new ActionResult<ResponseData<bool>>(serviceResponse);
    }

    // POST: api/ElectricProducts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize("admin")]
    public async Task<ActionResult<ResponseData<int>>> PostElectricProduct(ElectricProduct electricProduct)
    {
        var serviceResponse = await _productService.CreateProductAsync(electricProduct);

        if (!serviceResponse.Successfull)
        {
            return NotFound();
        }

        return new ActionResult<ResponseData<int>>(serviceResponse);
    }

    // DELETE: api/ElectricProducts/5
    [HttpDelete("{id}")]
    [Authorize("admin")]
    public async Task<ActionResult<bool>> DeleteElectricProduct(int id)
    {
        var serviceResponse = await _productService.DeleteProductAsync(id);

        if (!serviceResponse.Successfull)
        {
            return NotFound();
        }

        return new ActionResult<bool>(serviceResponse.Data);
    }

    //private bool ElectricProductExists(int id)
    //{
    //    return _context.ElectricProducts.Any(e => e.Id == id);
    //}
}
