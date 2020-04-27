using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallengeApi.BLL;
using CallengeApi.Helpers;
using CallengeApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CallengeApi
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

            services.AddMemoryCache();

            services.AddSingleton<AppSettings>();
            
            services.AddTransient<HackerNewEntity>();

            AppSettings appSettings = new AppSettings(Configuration);

            services.AddHttpClient(appSettings.HackerNewsName , c => {
                c.BaseAddress = new Uri(appSettings.HackerNewsUrl);
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            
            services.AddControllers().AddNewtonsoftJson(options => {

                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            });




        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }


       
    }
}
