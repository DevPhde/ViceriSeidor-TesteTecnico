using SuperHeroes.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces
{
    public interface IGetSuperHeroByIdHandler
    {
        Task<HeroAndSuperpowersResponse> Handle(int superHeroId);
    }
}
