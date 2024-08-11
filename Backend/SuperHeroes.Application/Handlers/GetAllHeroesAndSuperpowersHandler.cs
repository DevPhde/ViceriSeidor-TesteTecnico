using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class GetAllHeroesAndSuperpowersHandler : IGetAllHeroesAndSuperpowersHandler
    {
        private readonly ISuperHeroRepository _repository;

        public GetAllHeroesAndSuperpowersHandler(ISuperHeroRepository repository) => _repository = repository;

        public async Task<List<HeroAndSuperpowersResponse>> Handle()
        {
            List<Heroi> herosAndSuperpowers = await _repository.GetAllSuperHeroesAsync();

            return herosAndSuperpowers.Select(hs => hs.ToResponse()).ToList();
        }
    }
}
