using System.Text;
using System.Text.Json;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Services.ElectricProductService;

namespace WEB_253501_Rabets.UI.Services.ApiProductService;

public class ApiProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiProductService> _logger;
    private readonly string _pageSize;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IHttpClientFactory _httpClientFactory;
    public ApiProductService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiProductService> logger, IHttpClientFactory httpClientFactory)
    {
        //_httpClient = httpClient;
        _pageSize = configuration.GetSection("ItemsPerPage").Value;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
        _httpClientFactory = httpClientFactory;

        _httpClient = _httpClientFactory.CreateClient("MyApiClient");
    }

    public async Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        await Console.Out.WriteLineAsync(_httpClient.BaseAddress.ToString());
        
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}ElectricProducts/");
        
        urlString.Append($"{categoryNormalizedName}?");
        urlString.Append($"pageNo={pageNo}");
        urlString.Append($"&pageSize={_pageSize}");

        await Console.Out.WriteLineAsync(urlString.ToString());

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<ElectricProduct>>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<ProductListModel<ElectricProduct>>.Error($"Ошибка: {ex.Message}");
            }
        }
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<ProductListModel<ElectricProduct>>.Error($"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
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

    public Task UpdateProductAsync(int id, ElectricProduct product, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
