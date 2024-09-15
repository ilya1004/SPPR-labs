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

    // GET: api/ElectricProducts
    [HttpGet]
    [Route("{category}")]
    public async Task<ActionResult<ResponseData<ProductListModel<ElectricProduct>>>> GetElectricProducts(string? category, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 3)
    {
        var serviceResponse = await _productService.GetProductListAsync(category, pageNo, pageSize);
        return new ActionResult<ResponseData<ProductListModel<ElectricProduct>>>(serviceResponse);
    }

    // GET: api/ElectricProducts/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ElectricProduct>> GetElectricProduct(int id)
    {
        var serviceResponse = await _productService.GetProductByIdAsync(id);

        if (!serviceResponse.Successfull)
        {
            return NotFound();
        }

        return new ActionResult<ElectricProduct>(serviceResponse.Data);
    }

    // PUT: api/ElectricProducts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutElectricProduct(int id, ElectricProduct electricProduct)
    //{
    //    if (id != electricProduct.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(electricProduct).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!ElectricProductExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    //// POST: api/ElectricProducts
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<ElectricProduct>> PostElectricProduct(ElectricProduct electricProduct)
    //{
    //    _context.ElectricProducts.Add(electricProduct);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction("GetElectricProduct", new { id = electricProduct.Id }, electricProduct);
    //}

    //// DELETE: api/ElectricProducts/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteElectricProduct(int id)
    //{
    //    var electricProduct = await _context.ElectricProducts.FindAsync(id);
    //    if (electricProduct == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.ElectricProducts.Remove(electricProduct);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool ElectricProductExists(int id)
    //{
    //    return _context.ElectricProducts.Any(e => e.Id == id);
    //}
}
