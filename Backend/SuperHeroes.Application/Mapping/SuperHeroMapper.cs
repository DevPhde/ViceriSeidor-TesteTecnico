using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using System.Linq;

namespace SuperHeroes.Application.Mapping
{
    public static class SuperHeroMapper
    {
        public static HeroAndSuperpowersDTO ToDto(this SuperHeroRequest request)
        {
            return new HeroAndSuperpowersDTO(0, request.Nome, request.NomeHeroi, request.DataNascimento, request.Altura, request.Peso, request.Superpoderes.Select(sp => new SuperpowerDTO { Id = sp.Id, Descricao = sp.Descricao, SuperpoderNome = sp.SuperpoderNome }).ToList());
        }

        public static HeroAndSuperpowersResponse ToResponse(this Heroi entity)
        {

            var superpowers = entity.HeroisSuperpoderes.Select(hs => new SuperpowerResponse(hs.Superpoderes.Id, hs.Superpoderes.SuperpoderNome, hs.Superpoderes.Descricao)).ToList();

            return new HeroAndSuperpowersResponse(
                entity.Id, entity.Nome, entity.NomeHeroi, entity.DataNascimento, entity.Altura, entity.Peso, superpowers);
        }
    }
}
