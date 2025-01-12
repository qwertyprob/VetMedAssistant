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

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IMedcardRepository, MedcardRepository>();
            services.AddScoped<IMedcardService, MedcardService>();

            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<ISearchService, SearchService>();


            //services.AddScoped<IJwtProvider, JwtProvider>();
            

            services.AddSingleton<IEncrypt, Encrypt>();
          
            services.AddAutoMapper(typeof(Medcard.Bl.Mapping.MappingProfileBlazor));

            return services;
        }
    }
}
