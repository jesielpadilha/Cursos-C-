using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCatalog.Data;
using ProductCatalog.Repositories;
using Microsoft.OpenApi.Models;

namespace ProductCatalog
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(x => x.EnableEndpointRouting = false);
      services.AddResponseCompression();

      services.AddScoped<StoreDataContext, StoreDataContext>();
      services.AddTransient<ProductRepository, ProductRepository>();

      services.AddSwaggerGen(x =>
      {
        x.SwaggerDoc("v1", new OpenApiInfo { Title = "Balta Store", Version = "v1" });
      });
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
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Balta Store - V1");
      });
    }
  }
}
