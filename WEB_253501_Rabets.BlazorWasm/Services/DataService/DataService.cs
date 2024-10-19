using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WEB_253501_Rabets.BlazorWasm.Entities;
using WEB_253501_Rabets.BlazorWasm.Models;
using WEB_253501_Rabets.BlazorWasm.Services.Authentication;

namespace WEB_253501_Rabets.BlazorWasm.Services.DataService;

public class DataService : IDataService
{
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<ElectricProduct> Products { get; set; } = new List<ElectricProduct>();
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; } = 1;

    private Category? _selectedCategory = null;
    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                DataLoaded?.Invoke();
            }
        }
    }

    public event Action? DataLoaded;

    private readonly HttpClient _httpClient;
    private readonly ITokenAccessor _tokenAccessor;
    private readonly IConfiguration _configuration;
    private readonly string _pageSize;
    private readonly JsonSerializerOptions _serializerOptions;

    public DataService(IConfiguration configuration, ITokenAccessor tokenAccessor, HttpClient httpClient)
    {
        _configuration = configuration;
        _pageSize = _configuration.GetSection("ItemsPerPage").Value!;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        _httpClient = httpClient;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<ResponseData<ProductListModel<ElectricProduct>>> GetProductListAsync()
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}ElectricProducts/");

        urlString.Append(SelectedCategory == null ? "all?" : $"{SelectedCategory.NormalizedName}?");

        urlString.Append($"pageNo={CurrentPage}&");
        urlString.Append($"pageSize={_pageSize}");
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<ElectricProduct>>>(_serializerOptions);
                Products = data.Data.Items;
                TotalPages = data.Data.TotalPages;
                CurrentPage = data.Data.CurrentPage;
                DataLoaded?.Invoke();
                return ResponseData<ProductListModel<ElectricProduct>>.Success(data.Data);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return ResponseData<ProductListModel<ElectricProduct>>.Error($"Ошибка: {ex.Message}");
            }
        }
        Console.WriteLine($"Данные не получены от сервера. Error: {response.StatusCode}");
        return ResponseData<ProductListModel<ElectricProduct>>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
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
                Categories = data;
                DataLoaded?.Invoke();
                return ResponseData<List<Category>>.Success(data!);
            }
            catch (JsonException ex)
            {
                return ResponseData<List<Category>>.Error($"Ошибка: {ex.Message}");
            }
        }
        return ResponseData<List<Category>>.Error($"Данные не получены от сервера. Error: {response.StatusCode}");
    }
}