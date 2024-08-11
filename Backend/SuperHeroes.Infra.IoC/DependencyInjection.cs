using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroes.Application.Handlers;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Infra.Data.Context;
using SuperHeroes.Infra.Data.Interfaces;
using SuperHeroes.Infra.Data.Repositories;

namespace SuperHeroes.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //DB CONTEXT
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServer"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));



            // HANDLERS
            services.AddScoped<ICreateSuperHeroHandler, CreateSuperHeroHandler>();
            services.AddScoped<ICreateSuperpowerHandler, CreateSuperpowerHandler>();
            services.AddScoped<IGetAllSuperpowersHandler, GetAllSuperpowersHandler>();
            services.AddScoped<IRemoveSuperpowerHandler, RemoveSuperpowerHandler>();
            services.AddScoped<IGetAllHeroesAndSuperpowersHandler, GetAllHeroesAndSuperpowersHandler>();
            services.AddScoped<IGetSuperHeroByIdHandler, GetSuperHeroByIdHandler>();
            services.AddScoped<IUpdateSuperHeroHandler, UpdateSuperHeroHandler>();
            services.AddScoped<IRemoveSuperHeroHandler, RemoveSuperHeroHandler>();


            // REPOSITORIES
            services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
