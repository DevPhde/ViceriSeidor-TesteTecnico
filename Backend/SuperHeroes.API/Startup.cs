using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SuperHeroes.API.HubConfig;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Infra.IoC;
using System;

namespace SuperHeroes.API
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


            services.AddInfrastructure(Configuration);

            services.AddControllers();

            services.AddCors();
            services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(10);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperHeroes API", Version = "v1" });
                c.EnableAnnotations();
            });

            //WEBSOCKET
            services.AddScoped<IHeroesHub, HeroesHub>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(sui =>
                {
                    sui.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperHeroes");
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HeroesHub>("/heroesHub");
            });
        }
    }
}
