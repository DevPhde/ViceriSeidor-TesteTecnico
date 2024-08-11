using SuperHeroes.Application.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.SuperHeroes
{
    public interface IGetAllHeroesHandler
    {
        Task<List<HeroResponse>> Handle();
    }
}
