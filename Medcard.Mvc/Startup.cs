using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Repositories;
using Medcard.DbAccessLayer;
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

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    private string ConnectionDb { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IMedcardRepository, MedcardRepository>();
        services.AddScoped<IMedcardServiceMvc, MedcardServiceMvc>();

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAuthServiceMvc, AuthServiceMvc>();

        services.AddScoped<ISearchRepository, SearchRepository>();
        services.AddScoped<ISearchServiceMvc, SearchServiceMvc>();


        services.AddSingleton<IHostingServiceMvc, HostingServiceMvc>();
        services.AddSingleton<IEncrypt, Encrypt>();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Authorization/Auth";
            });

        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(MappingProfileMvc));

        // Получение строки подключения после регистрации IHostingServiceMvc
        var hostingService = services.BuildServiceProvider().GetService<IHostingServiceMvc>();
        ConnectionDb = hostingService.GetEnvironmentVariable();
        

        // Использование строки подключения для DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(ConnectionDb));

        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
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

        app.UseEndpoints(endpoints =>
        {
            // Маршруты
            endpoints.MapControllerRoute(
                name: "medcardUpdateRoute",
                pattern: "Medcard/Update/{id}",
                defaults: new { controller = "Medcard", action = "Update" });

            endpoints.MapControllerRoute(
                name: "searchRoute",
                pattern: "Search/SearchMedcard/{searchItem?}",
                defaults: new { controller = "Search", action = "SearchMedcard" });

            endpoints.MapControllerRoute(
                name: "medcardDefaultRoute",
                pattern: "{controller=Medcard}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "authRoute",
                pattern: "{controller=Authorization}/{action=Auth}");
        });
    }
}
