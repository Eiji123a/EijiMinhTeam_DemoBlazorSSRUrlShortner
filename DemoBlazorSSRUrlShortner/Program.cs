using DemoBlazorSSRUrlShortner.Components;
using DemoBlazorSSRUrlShortner.Data;
using DemoBlazorSSRUrlShortner.Repository;
using DemoBlazorSSRUrlShortner.Services;
using DemoBlazorSSRUrlShortner.UrlHelpers;
using Microsoft.EntityFrameworkCore;
using NetcodeHub.Packages.Extensions.Clipboard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbcontext>(option => 
option.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUrlHelper, UrlHelper>();
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddNetcodeHubClipboardService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/{shortcode}", async (string shortcode, IUrlService urlService) =>
{
    var result = await urlService.GetOriginalUrlAsync(shortcode);
    return result == string.Empty ? Results.NotFound() : Results.Redirect(result!);
}); 

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
