using IpInfoCore.Infraestructure;
using IpInfoCore.Infraestructure.EventRegister;
using IpStatsService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using net_api.Infraestructure;
using net_api.Infraestructure.ExternalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IpStatsService
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IpStatsService", Version = "v1" });
            });

            services.AddSingleton<IEventRegister, EventRegisterLight>(EventRegisterFactory);
            services.AddSingleton<IIpReportService, IpReportService>();
            services.AddSingleton<ICache, RedisCache>(CacheFactory);
            services.AddSingleton<IAPIRequestCreator, APIRequestCreator>();

        }
        private EventRegisterLight EventRegisterFactory(IServiceProvider arg)
        {
            return new EventRegisterLight(Env.RedisStats);
        }

        private RedisCache CacheFactory(IServiceProvider arg)
        {
            return new RedisCache(Env.RedisInfo);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IpStatsService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
