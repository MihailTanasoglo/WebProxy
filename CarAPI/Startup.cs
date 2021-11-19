using CarAPI.Repositories;
using CarAPI.Services;
using CarAPI.Settings;
using Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAPI
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
            services.AddHttpContextAccessor();
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.Configure<SyncServiceSettings>(Configuration.GetSection("SyncServiceSettings"));

            services.AddSingleton<IMongoDbSettings>(provider => 
            provider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton<ISyncServiceSettings>(provider => 
            provider.GetRequiredService<IOptions<SyncServiceSettings>>().Value);

            services.AddScoped<IMongoRepository<Car>, MongoRepository<Car>>();

            services.AddScoped<ISyncService<Car>, SyncService<Car>>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
