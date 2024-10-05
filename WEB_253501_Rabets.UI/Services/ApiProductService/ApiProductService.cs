using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Services.ApiFileService;
using WEB_253501_Rabets.UI.Services.Authentication;

namespace WEB_253501_Rabets.UI.Services.ApiProductService;

public class ApiProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiProductService> _logger;
    private readonly string _pageSize;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFileService _fileService;
    private readonly ITokenAccessor _tokenAccessor;
    public ApiProductService(IConfiguration configuration, ILogger<ApiProductService> logger, IHttpClientFactory httpClientFactory, IFileService fileService, ITokenAccessor tokenAccessor)
    {
        _configuration = configuration;
        _pageSize = _configuration.GetSection("ItemsPerPage").Value!;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
        _httpClientFactory = httpClientFactory;

        _httpClient = _httpClientFactory.CreateClient("MyApiClient");
        _fileService = fileService;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
    {        
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}ElectricProducts/");
        
        urlString.Append($"{categoryNormalizedName}?");
        urlString.Append($"pageNo={pageNo}");
        urlString.Append($"&pageSize={_pageSize}");

        //await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

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
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<ProductListModel<ElectricProduct>>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }


    public async Task<ResponseData<int>> CreateProductAsync(ElectricProduct product, IFormFile? formFile)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}ElectricProducts/");

        if (formFile != null)
        {
            var imageUrl = await _fileService.SaveFileAsync(formFile);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                product.ImagePath = imageUrl;
            }
        }

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.PostAsJsonAsync(new Uri(urlString.ToString()), product, _serializerOptions);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<int>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<int>.Error($"Ошибка: {ex.Message}");
            }
        }

        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<int>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }

    public async Task<ResponseData<bool>> DeleteProductAsync(int id)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}ElectricProducts/");

        urlString.Append($"{id}");

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.DeleteAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<bool>.Error($"Ошибка: {ex.Message}");
            }
        }
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<bool>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }

    public async Task<ResponseData<ElectricProduct>> GetProductByIdAsync(int id)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}ElectricProducts/");

        urlString.Append($"{id}");

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ElectricProduct>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<ElectricProduct>.Error($"Ошибка: {ex.Message}");
            }
        }
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<ElectricProduct>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }

    public async Task<ResponseData<bool>> UpdateProductAsync(int id, ElectricProduct product, IFormFile? formFile)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}ElectricProducts/{id}");

        if (formFile != null)
        {
            var imageUrl = await _fileService.SaveFileAsync(formFile);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                product.ImagePath = imageUrl;
            }
        }

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.PutAsJsonAsync(new Uri(urlString.ToString()), product, _serializerOptions);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<bool>.Error($"Ошибка: {ex.Message}");
            }
        }

        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<bool>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }
}
