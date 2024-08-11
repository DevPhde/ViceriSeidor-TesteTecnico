using SuperHeroes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Interfaces
{
    public interface IHeroSuperpowerRepository
    {
        Task AddHeroSuperpowerAsyncWithoutSaveChanges(List<HeroiSuperpoder> heroesAndSuperpowers);
        Task<List<HeroiSuperpoder>> GetHeroSuperpowersByHeroId(int heroId);
        void RemoveHeroSuperpowersByIdsWithoutSaveChanges(List<HeroiSuperpoder> heroSuperpowers);
    }
}
