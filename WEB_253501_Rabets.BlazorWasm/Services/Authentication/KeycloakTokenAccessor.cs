using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using WEB_253501_Rabets.BlazorWasm.Models;

namespace WEB_253501_Rabets.BlazorWasm.Services.Authentication;

public class KeycloakTokenAccessor : ITokenAccessor
{
    private readonly HttpClient _httpClient;
    private readonly KeycloakData _keycloakData;
    //private readonly HttpContext _httpContext;

    public KeycloakTokenAccessor(IOptions<KeycloakData> options, HttpClient httpClient)
    {
        _keycloakData = options.Value;
        //_httpContext = httpContextAccessor.HttpContext!;
        _httpClient = httpClient;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        //if (_httpContext.User.Identity!.IsAuthenticated)
        //{
        //    return await _httpContext.GetTokenAsync("access_token");
        //}

        var requestUri = $"{_keycloakData.Host}/realms/{_keycloakData.Realm}/protocol/openid-connect/token";

        HttpContent content = new FormUrlEncodedContent([
            new ("client_id", _keycloakData.ClientId),
            new ("grant_type", "client_credentials"),
            new ("client_secret", _keycloakData.ClientSecret),
            ]);

        var response = await _httpClient.PostAsync(requestUri, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(response.StatusCode.ToString());
        }

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonNode.Parse(jsonString)!["access_token"]!.GetValue<string>();
    }

    public async Task SetAuthorizationHeaderAsync(HttpClient httpClient)
    {
        string token = await GetAccessTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
    }
}
