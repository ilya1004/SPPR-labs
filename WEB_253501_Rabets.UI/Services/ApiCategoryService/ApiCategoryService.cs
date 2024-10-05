using System.Text.Json;
using System.Text;
using WEB_253501_Rabets.Domain.Entities;
using WEB_253501_Rabets.Domain.Models;
using WEB_253501_Rabets.UI.Services.Authentication;

namespace WEB_253501_Rabets.UI.Services.ApiCategoryService;

public class ApiCategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiCategoryService> _logger;
    private readonly string _pageSize;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenAccessor _tokenAccessor;
    public ApiCategoryService(IConfiguration configuration, ILogger<ApiCategoryService> logger, IHttpClientFactory httpClientFactory, ITokenAccessor tokenAccessor)
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
        _tokenAccessor = tokenAccessor;
    }

    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Categories");

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var data = await response.Content.ReadFromJsonAsync<List<Category>>(_serializerOptions);
                return ResponseData<List<Category>>.Success(data!);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<List<Category>>.Error($"Ошибка: {ex.Message}");
            }
        }
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<List<Category>>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }
}

