using BlackDigital.Blazor;
using BlackDigital.Blazor.DataBuilder;
using BlackDigital.Blazor.IndexedDB;
using BlackDigital.Rest;
using Example;
using Example.Models;
using Example.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new RestClient("https://openlibrary.org"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => ComponentDataBuilder.New());

builder.Services.AddRestService(restService => restService
    .AddService<IOpenLibrary>()
);

await builder.Build().RunAsync();