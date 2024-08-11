using SuperHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Infra.Data.Interfaces
{
    public interface ISuperpowerRepository
    {
        Task<List<Superpoder>> GetAllSuperpowersAsync();
        Task<Superpoder> AddSuperpowerAsync(Superpoder superpower);
        Task RemoveSuperpowerAsync(Superpoder superpower);
        Task<Superpoder> GetSuperpowerByIdAsync(int id);
        Task<Superpoder> GetSuperpowerByNameAsync(string name);

    }
}
