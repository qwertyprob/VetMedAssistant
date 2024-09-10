using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
using Medcard.DbAccessLayer.Mapping;
using Medcard.DbAccessLayer.Services;
using Medcard.Mvc.Mapping;
using Medcard.Mvc.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MedcardMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMedcardRepository, MedcardRepository>();
            services.AddScoped<IMedcardServiceMvc, MedcardServiceMvc>();

            services.AddAutoMapper(typeof(MappingProfileMvc));


            services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("MedcardConnectionString")));


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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "medcardUpdateRoute",
                    pattern: "Medcard/Update/{id}",
                    defaults: new
                    {
                        controller = "Medcard",
                        action = "Update"
                    });

                endpoints.MapControllerRoute(
                    name: "medcardRoute",
                    pattern: "Medcard/More/{ownerId}",
                    defaults: new
                    {
                        controller = "Medcard",
                        action = "More"
                    });
                //Authorization route by default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authorization}/{action=Index}");

                // Medcard route by default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Medcard}/{action=Index}/{id?}");

               
            });

        }
    }
}