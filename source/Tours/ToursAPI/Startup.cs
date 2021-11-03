using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToursWeb.ComponentsBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToursWeb.ModelsDB;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToursWeb.ImpRepositories;
using ToursWeb.Repositories;

namespace ToursAPI
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
            
            AddLoggerExtensions(services);
            AddRepositoryExtensions(services); 
            AddControllerExtensions(services);
            
            /*services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            */
            services.AddDbContext<ToursContext>(option => option.UseNpgsql(Configuration["Connections:Manager"]));
            //services.AddSingleton(provider => { return user; });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "ToursAPI", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToursAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void AddLoggerExtensions(IServiceCollection services)
        {
            var log = new LoggerConfiguration()
                .WriteTo.File(Configuration["Logger"])
                .CreateLogger();
            
            services.AddLogging(x =>
            {
                x.AddSerilog(logger: log, dispose: true);
            });
        }
        
        private void AddControllerExtensions(IServiceCollection services)
        {
            services.AddScoped<UserController>();
            services.AddScoped<TransferManagerController>();
            services.AddScoped<TouristController>();
            services.AddScoped<ManagerController>();
            services.AddScoped<GuestController>();
        }
        
        private void AddRepositoryExtensions(IServiceCollection services)
        {
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFunctionsRepository, FunctionsRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IPlaneRepository, PlaneRepository>();
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<ITrainRepository, TrainRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }
    }
}