using Medcard.DbAccessLayer;
using Medcard.DbAccessLayer.Dto;
using Medcard.DbAccessLayer.Entities;
using Medcard.DbAccessLayer.Interfaces;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddScoped<IMedcardRepository<OwnerDto, PetDto, DrugsDto, TreatmentsDto>, MedcardRepository>();
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("MedcardConnectionString"));
                }


            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                    pattern: "Medcard/UpdateMedcard/{ownerId}",
                    defaults: new
                    {
                        controller = "Medcard",
                        action = "UpdateMedcard"
                    });

                endpoints.MapControllerRoute(
                    name: "medcardRoute",
                    pattern: "Medcard/More/{ownerId}",
                    defaults: new
                    {
                        controller = "Medcard",
                        action = "More"
                    });
                // Маршрут по умолчанию для контроллера Medcard
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Medcard}/{action=Index}/{id?}");

               
            });

        }
    }
}