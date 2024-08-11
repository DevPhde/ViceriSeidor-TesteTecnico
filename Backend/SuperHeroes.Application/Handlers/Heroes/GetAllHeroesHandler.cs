using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.SuperHeroes
{
    public class GetAllHeroesHandler : IGetAllHeroesHandler
    {
        private readonly IHeroRepository _heroRepository;
        public GetAllHeroesHandler(IHeroRepository heroRepository) => _heroRepository = heroRepository;
        public async Task<List<HeroResponse>> Handle()
        {
            List<Heroi> heroes = await _heroRepository.GetAllHeroesAsync();

            return heroes.Select(hs => hs.ToResponse()).ToList();
        }
    }
}
