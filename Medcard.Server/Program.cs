using Medcard.Server.Components;
using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Mapping;
using Medcard.DbAccessLayer.Repositories;
using Medcard.Bl.Abstraction;
using Medcard.Bl.Mapping;
using Medcard.Bl.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text;
using Medcard.Server.Dependency;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//All my DI 
builder.Services.AddServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();// Server-side components

builder.Services.AddScoped<HttpClient>();


//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

//var jwtOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;


//var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

builder.Services.AddAntiforgery();


builder.Services.AddAuthentication();


builder.Services.AddHttpClient();

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();


builder.Services.AddBlazoredLocalStorage();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MedcardConnectionString")));

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

app.UseSession();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
