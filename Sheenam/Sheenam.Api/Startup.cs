using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Sheenam.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)=>
            Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            OpenApiInfo apiInfo = new OpenApiInfo
           {
               Title = "Sheenam.Api",
               Version = "v1",
           };
            services.AddControllers();

            services.AddSwaggerGen(opions =>
            {
                opions.SwaggerDoc(
                   name:
                   "v1",
                   info: apiInfo);
            });
        }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opions =>
                {
                    opions.SwaggerEndpoint(
                        url: "/swagger/v1/swagger.json",
                        name: "Sheenam.Api v1");
                });
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }
    }
}
