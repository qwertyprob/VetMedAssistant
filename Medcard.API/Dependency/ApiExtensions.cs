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
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtOptions = config.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                            Console.WriteLine("refreshed");
                            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                                context.Token = authHeader.Substring("Bearer ".Length).Trim();
                            else
                                context.Token = context.Request.Cookies["Jwt"];

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.Redirect("/login");
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.Response.Redirect("/login");
                            return Task.CompletedTask;
                        },
                    };
                });

            services.AddAuthorization();
        }

    }
}
