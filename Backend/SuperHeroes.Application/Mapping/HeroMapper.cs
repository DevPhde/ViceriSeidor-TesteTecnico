using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;

namespace SuperHeroes.Application.Mapping
{
    public static class HeroMapper
    {
        public static HeroDTO ToDto(this Heroi hero)
        {
            return new HeroDTO(hero.Id, hero.Nome, hero.NomeHeroi, hero.DataNascimento, hero.Altura, hero.Peso);
        }

        public static HeroResponse ToResponse(this HeroDTO dto)
        {
            return new HeroResponse(dto.Id, dto.Nome, dto.NomeHeroi, dto.DataNascimento, dto.Altura, dto.Peso);
        }
    }

}
