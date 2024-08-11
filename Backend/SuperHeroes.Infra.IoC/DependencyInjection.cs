using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroes.Application.Handlers.Heroes;
using SuperHeroes.Application.Handlers.SuperHeroes;
using SuperHeroes.Application.Handlers.Superpowers;
using SuperHeroes.Application.Interfaces.Heroes;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.Interfaces.Superpowers;
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
            services.AddScoped<IGetAllHeroesHandler, GetAllHeroesHandler>();
            services.AddScoped<IGetHeroByIdHandler, GetHeroByIdhandler>();
            services.AddScoped<ICreateHeroHandler, CreateHeroHandler>();


            services.AddScoped<ICreateSuperpowerHandler, CreateSuperpowerHandler>();
            services.AddScoped<IGetAllSuperpowersHandler, GetAllSuperpowersHandler>();
            services.AddScoped<IRemoveSuperpowerHandler, RemoveSuperpowerHandler>();
            
            services.AddScoped<IUpdateHeroHandler, UpdateHeroHandler>();
            services.AddScoped<IRemoveHeroHandler, RemoveHeroHandler>();


            // REPOSITORIES
            services.AddScoped<IHeroSuperpowerRepository, HeroSuperpowerRepository>();
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<ISuperpowerRepository, SuperpowerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
