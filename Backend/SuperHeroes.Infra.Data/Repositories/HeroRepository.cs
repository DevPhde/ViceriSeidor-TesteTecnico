using Microsoft.EntityFrameworkCore;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Context;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly AppDbContext _context;

        public HeroRepository(AppDbContext context) => _context = context;

        public async Task<Heroi> AddHeroAsyncWithoutSaveChanges(Heroi hero)
        {
            await _context.AddAsync(hero);
            return hero;
        }

        public async Task<Heroi> GetHeroByHeroName(string heroName) => await _context.Herois
                                                                    .Include(h => h.HeroisSuperpoderes)
                                                                    .ThenInclude(hs => hs.Superpoderes)
                                                                    .FirstOrDefaultAsync(x => x.NomeHeroi == heroName);

        public async Task<Heroi> GetHeroByIdAsync(int heroId) => await _context.Herois
                                                                    .Include(h => h.HeroisSuperpoderes)
                                                                    .ThenInclude(hs => hs.Superpoderes)
                                                                    .FirstOrDefaultAsync(x => x.Id == heroId);

        public void RemoveHeroWithoutSaveChanges(Heroi hero) => _context.Remove(hero);

        public async Task<List<Heroi>> GetAllHeroesAsync() => await _context.Herois
                                                                    .Include(h => h.HeroisSuperpoderes)
                                                                    .ThenInclude(hs => hs.Superpoderes)
                                                                    .ToListAsync();



    }
}
