using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using System.Linq;

namespace SuperHeroes.Application.Mapping
{
    public static class HeroMapper
    {
        public static HeroDTO ToDto(this HeroRequest request)
        {
            return new HeroDTO(request.Id, request.Nome, request.NomeHeroi, request.DataNascimento, request.Altura, request.Peso, request.Superpoderes.Select(x => new SuperpowerDTO(x.Id, x.SuperpoderNome, x.Descricao)).ToList());
        }

        public static HeroResponse ToResponse(this HeroDTO dto)
        {
            return new HeroResponse(dto.Id, dto.Nome, dto.NomeHeroi, dto.DataNascimento, dto.Altura, dto.Peso, dto.Superpowers.Select(x => new SuperpowerResponse(x.Id, x.SuperpoderNome, x.Descricao)).ToList());
        }
        public static HeroResponse ToResponse(this Heroi hero)
        {
            return new HeroResponse(hero.Id, hero.Nome, hero.NomeHeroi, hero.DataNascimento, hero.Altura, hero.Peso, hero.HeroisSuperpoderes.Select(x => new SuperpowerResponse(x.Superpoderes.Id, x.Superpoderes.SuperpoderNome, x.Superpoderes.Descricao)).ToList());
        }
    }

}
