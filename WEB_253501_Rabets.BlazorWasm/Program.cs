using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WEB_253501_Rabets.BlazorWasm;
using WEB_253501_Rabets.BlazorWasm.Extensions;
using WEB_253501_Rabets.BlazorWasm.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));

var uriData = builder.Configuration.GetSection("UriData").Get<UriData>();

builder.Services.AddScoped(sp => new HttpClient { 
    //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    BaseAddress = new Uri(uriData!.ApiUri)
});

builder.RegisterCustomService();

//builder.Services.AddHttpClient("MyApiClient", client =>
//{
//    client.BaseAddress = new Uri(uriData!.ApiUri);
//});

builder.Services.AddOidcAuthentication(options => {
    // Configure your authentication provider options here. 
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Keycloak", options.ProviderOptions);
});


await builder.Build().RunAsync();
