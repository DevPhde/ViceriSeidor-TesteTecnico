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
    public class HeroSuperpowerRepository : IHeroSuperpowerRepository
    {
        private readonly AppDbContext _context;
        public HeroSuperpowerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddHeroSuperpowerAsyncWithoutSaveChanges(List<HeroiSuperpoder> heroSuperpowers)
        {
            await _context.HeroisSuperpoderes.AddRangeAsync(heroSuperpowers);
        }

        public async Task<List<HeroiSuperpoder>> GetHeroSuperpowersByHeroId(int heroId)
        {
            return await _context.HeroisSuperpoderes.Where(hp => hp.HeroiId == heroId).ToListAsync();
        }

        public void RemoveHeroSuperpowersByIdsWithoutSaveChanges(List<HeroiSuperpoder> heroSuperpowers)
        {
            _context.HeroisSuperpoderes.RemoveRange(heroSuperpowers);
        }
    }
}
