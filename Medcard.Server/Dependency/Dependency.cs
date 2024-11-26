using Medcard.Bl.Abstraction;
using Medcard.Bl.Services;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Repositories;
using Medcard.DbAccessLayer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Medcard.Server.Dependency
{
    public  static class Dependency
    { 
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();// Server-side components


            services.AddScoped<IMedcardRepository, MedcardRepository>();
            services.AddScoped<IMedcardService, MedcardService>();

            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<ISearchService, SearchService>();




            // Add distributed memory cache
            services.AddDistributedMemoryCache();

            // Configure session settings
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
            services.AddAutoMapper(typeof(Medcard.Bl.Mapping.MappingProfileBlazor));

            return services;
        }
    }
}
