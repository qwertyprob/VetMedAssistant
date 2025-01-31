using Medcard.Server.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddAntiforgery();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

//HttpClientFactory 
builder.Services.AddHttpClient("Medcard", client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/api");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


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
app.UseAuthorization();  

app.UseAntiforgery(); 



app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

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
