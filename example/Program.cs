using BlackDigital.Rest;
using BlackDigital.Blazor.DataBuilder;
using Example;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BlackDigital.Blazor.Rest;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new RestClient("https://openlibrary.org"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => ComponentDataBuilder.New());

builder.Services.AddLogging();

builder.Services.AddRestService(confg =>
    confg.AddBaseUrl("https://openlibrary.org")
         .AddThownType(RestThownType.OnlyBusiness)
);
//builder.Services.TryAdd(ServiceDescriptor.Scoped(typeof(RestService<>), typeof(RestService<>)));


await builder.Build().RunAsync();