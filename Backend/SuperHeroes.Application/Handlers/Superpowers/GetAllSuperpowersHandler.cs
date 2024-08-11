using SuperHeroes.Application.Interfaces.Superpowers;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.Superpowers
{
    public class GetAllSuperpowersHandler : IGetAllSuperpowersHandler
    {
        private readonly ISuperpowerRepository _superpowerRepository;

        public GetAllSuperpowersHandler(ISuperpowerRepository superpowerRepository) => _superpowerRepository = superpowerRepository;

        public async Task<List<SuperpowerResponse>> Handle()
        {
            List<Superpoder> superpowers = await _superpowerRepository.GetAllSuperpowersAsync();

            return superpowers.Select(sp => sp.ToResponse()).ToList();
        }
    }
}
