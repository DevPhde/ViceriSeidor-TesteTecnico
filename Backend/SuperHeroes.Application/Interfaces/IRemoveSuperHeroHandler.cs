using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces
{
    public interface IRemoveSuperHeroHandler
    {
        Task handle(int superHeroId);
    }
}
