using HellBlaze.Components;
using HellBlaze.Logic;
using HellBlaze.Models;
using HellBlaze.Services;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

builder.Services.AddMudServices(config => { config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight; });
builder.Services.AddScoped<GameData>();
builder.Services.AddScoped<State>();
builder.Services.AddScoped<RandomRank>();
builder.Services.AddSingleton<LoadoutService>();
builder.Services.AddControllers();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();