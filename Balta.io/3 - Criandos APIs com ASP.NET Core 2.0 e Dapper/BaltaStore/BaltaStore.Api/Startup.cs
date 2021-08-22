using System;
using System.IO;
using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Infra.Repositories;
using BaltaStore.Infra.Services;
using BaltaStore.Infra.StoreContext.DataContexts;
using BaltaStore.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace BaltaStore.Api
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
            services.AddMvc(x => x.EnableEndpointRouting = false);

            services.AddResponseCompression();

            services.AddScoped<BaltaDataContext, BaltaDataContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Balta Store", Version = "v1", });
            });

            services.AddElmahIo(o =>
            {
                o.ApiKey = "53056b810cae445c9446a529fca1509a";
                o.LogId = new Guid("35e87141-95d2-442f-ac18-7ddcd7307a3d");
            });

            Settings.ConnectionString = Configuration["ConnectionString"];
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();


            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Balta Store - v1");
            });

            app.UseElmahIo();
        }
    }
}
