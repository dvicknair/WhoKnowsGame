using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WhoKnowsGame.Components;
using WhoKnowsGame.Data;
using WhoKnowsGame.Services;
using WhoKnowsGame.Shared.Interfaces;
using WhoKnowsGame.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.AddSqlServerDbContext<WhoKnowsDbContext>("whoknowsdatabase");

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WhoKnowsDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    var game = context.Riddles.FirstOrDefault();
    if (game == null)
    {
        context.Set<Game>().Add(new Game
        {
            Name = "Who Knows Bob",
            Riddles = [
                new() {
                        Question = "What is Bob's favorite color?",
                        Answers = [
                            new() { Text = "Red" },
                            new() { Text = "Blue" },
                            new() { Text = "Green" },
                            new() { Text = "Purple" },
                        ]
                    },
                    new() {
                        Question = "What is Bob's favorite food?",
                        Answers = [
                            new() { Text = "Pizza" },
                            new() { Text = "Tacos" },
                            new() { Text = "Ice Cream" },
                            new() { Text = "Hot Dogs" },
                        ]
                    }
            ]
        });
        context.SaveChanges();
    }
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapGet("/game/{gameId}", ([FromServices] IGameService gameService, int gameId) => gameService.GetGame(gameId));

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WhoKnowsGame.Client._Imports).Assembly);

app.Run();
