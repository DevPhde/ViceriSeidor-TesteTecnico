using SuperHeroes.Application.ResponseModels;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.SuperHeroes
{
    public interface IGetHeroByIdHandler
    {
        Task<HeroResponse> Handle(int superHeroId);
    }
}
