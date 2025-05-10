using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using sg_transpo_rcl.Clients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton(_ => new TransportApiClient("https://eleazplayground.azurewebsites.net", new HttpClient()));

await builder.Build().RunAsync();