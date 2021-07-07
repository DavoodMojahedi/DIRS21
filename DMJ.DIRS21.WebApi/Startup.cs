using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DMJ.DIRS21.Common.Constants;
using DMJ.DIRS21.DataAccess.Repository;
using DMJ.DIRS21.Model.Configuration;
using DMJ.DIRS21.Service.Contracts;
using DMJ.DIRS21.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace DMJ.DIRS21.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json");

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = SwaggerConsts.Title,
                    Version = SwaggerConsts.Version,
                    Description = SwaggerConsts.Description,
                    Contact = new OpenApiContact()
                    {
                        Name = SwaggerConsts.ContactName,
                        Email = SwaggerConsts.ContactEmail
                    }
                });
            });
            services.AddOptions();

            services.Configure<MongoDbConfiguration>(Configuration.GetSection("MongoDbConfiguration"));


            services.AddMvc();

            RegisterServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(AppConsts.ExceptionHandler);

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(SwaggerConsts.JsonPath, SwaggerConsts.Version);
                option.EnableFilter();
                option.DefaultModelExpandDepth(-1);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IRepository,Repository>();
            services.AddScoped<IDishListService, DishListService>();
            services.AddScoped<IDishChangeService, DishChangeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
        }
    }
}
