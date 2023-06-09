using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vecttorAPI.Repositories;
using vecttorAPI.Services;

namespace vecttorAPI
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
            services.AddScoped<IRepositoryAsteroide, RepositoryAsteroide>();
            services.AddScoped<INasaCalls, NasaCalls>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    name: "v1", new OpenApiInfo
                    {
                        Title = "API Asteroides",
                        Version = "v1",
                        Description = "API Vecttor"
                    }
                    );
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint(
                            url: "/swagger/v1/swagger.json",
                            name: "Api v1"
                            );
                        c.RoutePrefix = "";
                    }
                    );

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
