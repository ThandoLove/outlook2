using OutlookTestBlazor.Components;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using OutlookTestBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddHttpClient<OutlookTestBlazor.Services.SageX3Service>();
builder.Services.AddScoped<OutlookTestBlazor.Services.SageX3Service>();

// If you use authentication uncomment and configure
// builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
// Serve Blazor WASM index
app.MapFallbackToFile("/index.html");

app.Run();
