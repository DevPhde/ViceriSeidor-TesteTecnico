using SuperHeroes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Interfaces
{
    public interface IHeroRepository
    {
        Task<Heroi> AddHeroAsyncWithoutSaveChanges(Heroi hero);
        Task<Heroi> GetHeroByHeroName(string heroName);
        void RemoveHeroWithoutSaveChanges(Heroi hero);


        Task<List<Heroi>> GetAllHeroesAsync();
        Task<Heroi> GetHeroByIdAsync(int heroId);
    }
}
