using System.Text;
using WEB_253501_Rabets.UI.Services.Authentication;

namespace WEB_253501_Rabets.UI.Services.ApiFileService;

public class ApiFileService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenAccessor _tokenAccessor;
    public ApiFileService(IHttpClientFactory httpClientFactory, ITokenAccessor tokenAccessor)
    {
        _httpClient = httpClientFactory.CreateClient("MyApiClient");
        _tokenAccessor = tokenAccessor;
    }
    public async Task DeleteFileAsync(string fileName)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Files/");

        urlString.Append($"{fileName}");

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, new Uri(urlString.ToString()));

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.SendAsync(httpRequestMessage);

        if (response.IsSuccessStatusCode)
        {
            await response.Content.ReadAsStringAsync();
        }
    }

    public async Task<string> SaveFileAsync(IFormFile formFile)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{_httpClient.BaseAddress!.AbsoluteUri}Files/")
        };
        
        //var extension = Path.GetExtension(formFile.FileName);
        //var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
        
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(formFile.OpenReadStream());
        content.Add(streamContent, "file", formFile.FileName);
        
        request.Content = content;

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return string.Empty;
    }
}
