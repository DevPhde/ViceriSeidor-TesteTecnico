using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.ResponseModels;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.Heroes
{
    public interface IUpdateHeroHandler
    {
        Task<HeroResponse> Handle(HeroDTO dto, int heroId);
    }
}
