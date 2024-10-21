using MickeyWebUtility;
using MickeyWebUtility.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<SingaporeItinerary>();
builder.Services.AddScoped<SGItineraryService>();
builder.Services.AddScoped<RenovationService>();
builder.Services.AddScoped<LazyAssemblyLoader>();

await builder.Build().RunAsync();