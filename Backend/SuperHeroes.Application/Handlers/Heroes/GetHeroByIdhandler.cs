using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.SuperHeroes
{
    public class GetHeroByIdhandler : IGetHeroByIdHandler
    {
        private readonly IHeroRepository _heroRepository;

        public GetHeroByIdhandler(IHeroRepository repository) => _heroRepository = repository;

        public async Task<HeroResponse> Handle(int superHeroId)
        {
            Heroi hero = await _heroRepository.GetHeroByIdAsync(superHeroId) ?? throw new NotFoundException("Herói não encontrado.");

            return hero.ToResponse();
        }
    }
}
