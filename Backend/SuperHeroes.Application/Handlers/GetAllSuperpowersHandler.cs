using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class GetAllSuperpowersHandler : IGetAllSuperpowersHandler
    {
        private readonly ISuperHeroRepository _repository;

        public GetAllSuperpowersHandler(ISuperHeroRepository repository) => _repository = repository;

        public async Task<List<SuperpowerResponse>> Handle()
        {
            List<Superpoder> superpowers = await _repository.GetAllSuperpowersAsync();

            return superpowers.Select(sp => sp.ToResponse()).ToList();
        }
    }
}
