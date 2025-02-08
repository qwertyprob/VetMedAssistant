using Medcard.Client.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Medcard.Client.Abstraction;
using Medcard.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Server;
using Blazored.SessionStorage;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAntiforgery();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddAuthenticationCore();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });


builder.Services.AddAuthorization();

builder.Services.AddBlazoredLocalStorage();

//HttpClientFactory 
builder.Services.AddHttpClient("Medcard", client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/api");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<IMedcardHttpService, MedcardHttpService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseCors("AllowAll");
app.UseAuthorization();  

app.UseAntiforgery();



app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

//app.Use(async (context, next) =>
//{
//    if (!context.User.Identity.IsAuthenticated && context.Request.Path != "/login")
//    {
//        context.Response.Redirect("/login");
//        return;
//    }

//    await next();
//});


app.Run();
