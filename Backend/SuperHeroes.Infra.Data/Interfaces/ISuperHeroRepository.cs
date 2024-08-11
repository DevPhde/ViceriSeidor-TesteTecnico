using SuperHeroes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Interfaces
{
    public interface ISuperHeroRepository
    {
        //HERO SUPERPOWER
        Task<HeroiSuperpoder> AddSuperHeroAsync(HeroiSuperpoder superHero);
        Task AddListOfSuperpowersAsyncTransaction(List<HeroiSuperpoder> heroesAndSuperpowers);
        Task<List<Heroi>> GetAllSuperHeroesAsync();
        Task<Heroi> GetSuperHeroByIdAsync(int id);
        Task<Heroi> UpdateSuperHeroAsync(Heroi hero);
        Task AddListOfSuperpowersWithoutSaveChanges(List<HeroiSuperpoder> heroesAndSuperpowers);


        //HERO
        Task<Heroi> AddHeroAsync(Heroi hero);
        Task<Heroi> GetHeroByIdAsync(int id);
        Task<Heroi> GetHeroByHeroName(string heroName);
        Task<List<Heroi>> GetAllHerosAsync();
        Task RollbackAddedHeroAsync(int heroId);
        void RemoveHeroWithoutSaveChanges(Heroi hero);



        //Superpower
        Task<List<Superpoder>> GetAllSuperpowersAsync();
        Task<Superpoder> AddSuperpowerAsync(Superpoder superpower);
        Task<Superpoder> GetSuperpowerByIdAsync(int id);
        Task<Superpoder> GetSuperpowerByNameAsync(string name);
        Task RemoveSuperpowerAsync(Superpoder superpower);
        Task RemoveHeroSuperpowersWithoutSaveChanges(List<int> superpowersIds, int superHeroId);


    }
}
