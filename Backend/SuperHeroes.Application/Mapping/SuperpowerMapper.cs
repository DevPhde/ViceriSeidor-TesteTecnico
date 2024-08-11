using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;

namespace SuperHeroes.Application.Mapping
{
    public static class SuperpowerMapper
    {

        public static SuperpowerDTO ToDto(this SuperpowerRequest request)
        {
            return new SuperpowerDTO(request.Id, request.SuperpoderNome, request.Descricao);
        }
        public static SuperpowerResponse ToResponse(this SuperpowerDTO dto)
        {
            return new SuperpowerResponse(dto.Id, dto.SuperpoderNome, dto.Descricao);
        }
        public static SuperpowerResponse ToResponse(this Superpoder entity)
        {
            return new SuperpowerResponse(entity.Id, entity.SuperpoderNome, entity.Descricao);
        }
    }
}
