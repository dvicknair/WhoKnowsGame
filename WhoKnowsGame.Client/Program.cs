using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WhoKnowsGame.Client.Services;
using WhoKnowsGame.Shared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IGameService, GameService>();

await builder.Build().RunAsync();
