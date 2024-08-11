using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using System.Linq;

namespace SuperHeroes.Application.Mapping
{
    public static class HeroMapper
    {
        public static HeroDTO ToDto(this Heroi hero)
        {
            return new HeroDTO(hero.Id, hero.Nome, hero.NomeHeroi, hero.DataNascimento, hero.Altura, hero.Peso, hero.HeroisSuperpoderes.Select(x => new SuperpowerDTO(x.Superpoderes.Id, x.Superpoderes.SuperpoderNome, x.Superpoderes.Descricao)).ToList());
        }

        public static HeroResponse ToResponse(this HeroDTO dto)
        {
            return new HeroResponse(dto.Id, dto.Nome, dto.NomeHeroi, dto.DataNascimento, dto.Altura, dto.Peso, dto.Superpowers.Select(x => new SuperpowerResponse(x.Id, x.SuperpoderNome, x.Descricao)).ToList() );
        }
    }

}
