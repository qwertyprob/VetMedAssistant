using Medcard.Bl.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace Medcard.Api.Dependency
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services,
                                                     IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                    };
                    options.Events = new JwtBearerEvents()
                    {   //Запись токена в куки 
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["Jwt"];
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            
                            context.Response.Redirect("user/login");
                            return Task.CompletedTask;
                        },

                        // Обработчик на случай ошибки авторизации (например, пользователь не авторизован)
                        OnChallenge = context =>
                        {
                            
                            context.Response.Redirect("user/login");
                            return Task.CompletedTask;
                        },
                    };

                });

            services.AddAuthorization();

            
        }
    }
}
