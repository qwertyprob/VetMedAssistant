using Medcard.Server.Components;
using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Mapping;
using Medcard.DbAccessLayer.Repositories;
using Medcard.Mvc.Abstractions;
using Medcard.Mvc.Mapping;
using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();// Server-side components


builder.Services.AddScoped<IMedcardRepository<OwnerDto>, MedcardRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddSingleton<IHostingServiceMvc, HostingServiceMvc>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();

// Add distributed memory cache
builder.Services.AddDistributedMemoryCache();

// Configure session settings
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authorization/Auth";
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Connect to the database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MedcardConnectionString"))
    .EnableSensitiveDataLogging(false));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Map server-side and WebAssembly components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Medcard.Client._Imports).Assembly);




app.Run();
