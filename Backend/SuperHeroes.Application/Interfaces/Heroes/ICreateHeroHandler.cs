using SuperHeroes.Application.DTOs;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.SuperHeroes
{
    public interface ICreateHeroHandler
    {
        Task<HeroDTO> Handle(HeroDTO dto);
    }
}
