using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Repositories;
using Medcard.Mvc.Mapping;
using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Medcard.Mvc.Abstractions;
using Medcard.DbAccessLayer;


var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
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

// Получение строки подключения
var hostingService = builder.Services.BuildServiceProvider().GetService<IHostingServiceMvc>();
var connectionDb = hostingService.GetEnvironmentVariable();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionDb));

// Добавление поддержки MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Конфигурация pipeline приложения
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

// Определение маршрутов
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
