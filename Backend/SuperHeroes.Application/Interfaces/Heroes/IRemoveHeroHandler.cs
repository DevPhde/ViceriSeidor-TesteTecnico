using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces.Heroes
{
    public interface IRemoveHeroHandler
    {
        Task Handle(int heroId);
    }
}
