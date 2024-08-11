using SuperHeroes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Interfaces
{
    public interface ICreateSuperHeroHandler
    {
        Task<HeroAndSuperpowersDTO> Handle(HeroAndSuperpowersDTO dto);
    }
}
