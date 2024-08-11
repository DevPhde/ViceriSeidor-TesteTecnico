using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class GetSuperHeroByIdHandler : IGetSuperHeroByIdHandler
    {
        private readonly ISuperHeroRepository _repository;

        public GetSuperHeroByIdHandler(ISuperHeroRepository repository) => _repository = repository;

        public async Task<HeroAndSuperpowersResponse> Handle(int superHeroId)
        {
            Heroi hero = await _repository.GetSuperHeroByIdAsync(superHeroId) ?? throw new NotFoundException("Super Heroi não encontrado.");

            return hero.ToResponse();
        }
    }
}
