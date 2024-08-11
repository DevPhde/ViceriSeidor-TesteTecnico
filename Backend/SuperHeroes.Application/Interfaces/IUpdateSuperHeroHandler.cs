using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces
{
    public interface IUpdateSuperHeroHandler
    {
        Task<HeroAndSuperpowersResponse> Handle(HeroAndSuperpowersDTO dto, int superHeroId);
    }
}
