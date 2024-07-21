using MickeyWebUtility;
using MickeyWebUtility.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure base address dynamically
builder.Services.AddScoped(sp =>
{
    var jsRuntime = sp.GetRequiredService<IJSRuntime>();
    var baseAddress = jsRuntime.InvokeAsync<string>("getBaseHref").Result;
    return new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + baseAddress) };
});

builder.Services.AddSingleton<SGItineraryService>();
builder.Services.AddScoped<LazyAssemblyLoader>();

await builder.Build().RunAsync();