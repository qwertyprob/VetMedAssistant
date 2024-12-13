using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Repositories;
using Medcard.Mvc.Abstractions;
using Medcard.Mvc.Mapping;
using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// ??????? 

builder.Services.AddScoped<IMedcardRepository, MedcardRepository>();
builder.Services.AddScoped<IMedcardServiceMvc, MedcardServiceMvc>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthServiceMvc, AuthServiceMvc>();

builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddScoped<ISearchServiceMvc, SearchServiceMvc>();

builder.Services.AddSingleton<IHostingServiceMvc, HostingServiceMvc>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();

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
builder.Services.AddAutoMapper(typeof(MappingProfileMvc));

//builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
//{
//    var hostingService = serviceProvider.GetRequiredService<IHostingServiceMvc>();
//    var connectionDb = hostingService.GetEnvironmentVariable();
//    options.UseNpgsql(connectionDb);
//});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MedcardConnectionString")));




builder.Services.AddControllersWithViews();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Medcard/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "medcardUpdateRoute",
    pattern: "Medcard/Update/{id}",
    defaults: new { controller = "Medcard", action = "Update" });

app.MapControllerRoute(
    name: "searchRoute",
    pattern: "Search/SearchMedcard/{searchItem?}",
    defaults: new { controller = "Search", action = "SearchMedcard" });

app.MapControllerRoute(
    name: "medcardDefaultRoute",
    pattern: "{controller=Medcard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "authRoute",
    pattern: "{controller=Authorization}/{action=Auth}");

app.Run();
