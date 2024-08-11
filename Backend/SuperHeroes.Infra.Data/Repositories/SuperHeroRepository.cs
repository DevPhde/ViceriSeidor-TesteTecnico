using Microsoft.EntityFrameworkCore;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Context;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Repositories
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public SuperHeroRepository(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        //SUPER HERO

        public async Task<List<Heroi>> GetAllSuperHeroesAsync() => await _context.Herois
                                                                    .Include(h => h.HeroisSuperpoderes)
                                                                    .ThenInclude(hs => hs.Superpoderes)
                                                                    .ToListAsync();
        public async Task<Heroi> GetSuperHeroByIdAsync(int id) => await _context.Herois
                                                                    .Include(h => h.HeroisSuperpoderes)
                                                                    .ThenInclude(hs => hs.Superpoderes)
                                                                    .FirstOrDefaultAsync(x => x.Id == id);


        public Task<Heroi> UpdateSuperHeroAsync(Heroi hero)
        {
            throw new NotImplementedException();
        }

        public Task<HeroiSuperpoder> AddSuperHeroAsync(HeroiSuperpoder superHero)
        {
            throw new NotImplementedException();
        }



        public async Task AddListOfSuperpowersAsyncTransaction(List<HeroiSuperpoder> heroesAndSuperpowers)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _context.HeroisSuperpoderes.AddRangeAsync(heroesAndSuperpowers);
                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                
                throw;
            }
        }

        public async Task AddListOfSuperpowersWithoutSaveChanges(List<HeroiSuperpoder> heroesAndSuperpowers)
        {
            await _context.HeroisSuperpoderes.AddRangeAsync(heroesAndSuperpowers);
        }


        // HERO
        public async Task<Heroi> AddHeroAsync(Heroi hero)
        {
            _context.Add(hero);
            await _context.SaveChangesAsync();
            return hero;
        }

        public void RemoveHeroWithoutSaveChanges(Heroi hero)
        {
            _context.Remove(hero);
        }

        public async Task RollbackAddedHeroAsync(int heroId)
        {
            var hero = await _context.Herois.FirstOrDefaultAsync(x => x.Id == heroId);
            if (hero != null)
            {
                _context.Remove(hero);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Heroi>> GetAllHerosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Heroi> GetHeroByHeroName(string heroName) => await _context.Herois.FirstOrDefaultAsync(x => x.NomeHeroi == heroName);

        public Task<Heroi> GetHeroByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        // SUPERPOWER
        public async Task<Superpoder> AddSuperpowerAsync(Superpoder superpower)
        {
            _context.Add(superpower);
            await _context.SaveChangesAsync();
            return superpower;
        }
        public async Task<Superpoder> GetSuperpowerByIdAsync(int id) => await _context.Superpoderes.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Superpoder> GetSuperpowerByNameAsync(string name) => await _context.Superpoderes.FirstOrDefaultAsync(x => x.SuperpoderNome == name);

        public async Task<List<Superpoder>> GetAllSuperpowersAsync() => await _context.Superpoderes.ToListAsync();

        public async Task RemoveSuperpowerAsync(Superpoder superpower)
        {
            _context.Remove(superpower);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveHeroSuperpowersWithoutSaveChanges(List<int> superpowersIds, int superHeroId)
        {
            var superpowers = await _context.HeroisSuperpoderes.Where(hp => hp.HeroiId == superHeroId).ToListAsync();
            _context.HeroisSuperpoderes.RemoveRange(superpowers);
        }


    }
}
