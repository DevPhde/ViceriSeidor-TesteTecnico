using Microsoft.EntityFrameworkCore;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Context;
using SuperHeroes.Infra.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Repositories
{
    public class SuperpowerRepository : ISuperpowerRepository
    {
        private readonly AppDbContext _context;

        public SuperpowerRepository(AppDbContext context) => _context = context;

        public async Task<List<Superpoder>> GetAllSuperpowersAsync() => await _context.Superpoderes.ToListAsync();

        public async Task<Superpoder> AddSuperpowerAsync(Superpoder superpower)
        {
            _context.Add(superpower);
            await _context.SaveChangesAsync();
            return superpower;
        }
        public async Task<Superpoder> GetSuperpowerByIdAsync(int id) => await _context.Superpoderes.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Superpoder> GetSuperpowerByNameAsync(string name) => await _context.Superpoderes.FirstOrDefaultAsync(x => x.SuperpoderNome == name);


        public async Task RemoveSuperpowerAsync(Superpoder superpower)
        {
            _context.Remove(superpower);
            await _context.SaveChangesAsync();
        }
    }
}
