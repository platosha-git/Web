using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ToursWeb.ModelsDB;
using ToursWeb.ComponentsBL;
using ToursWeb.Repositories;
using ToursWeb.ImpRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            services.AddDbContext<ToursContext>(option => option.UseNpgsql(config["Connections:Manager"]));
            
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            
            var log = new LoggerConfiguration()
                .WriteTo.File(config["Logger"])
                .CreateLogger();
            
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(logger: log, dispose: true));

            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IPlaneRepository, PlaneRepository>();
            services.AddScoped<ITrainRepository, TrainRepository>();
            services.AddScoped<IFunctionsRepository, FunctionsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddScoped<UserController>();
            services.AddScoped<GuestController>();
            services.AddScoped<TouristController>();
            services.AddScoped<ManagerController>();
            services.AddScoped<TransferManagerController>();

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
    }
}