using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class CreateSuperpowerHandler : ICreateSuperpowerHandler
    {
        private readonly ISuperHeroRepository _repository;

        public CreateSuperpowerHandler(ISuperHeroRepository repository) => _repository = repository;

        public async Task<SuperpowerResponse> Handle(SuperpowerDTO dto)
        {
            Superpoder superpower = await _repository.GetSuperpowerByNameAsync(dto.SuperpoderNome);
            if (superpower != null)
            {
                throw new ConflictException("Superpoder já cadastrado com esse nome.");
            }
            Superpoder newSuperpower = new Superpoder(dto.SuperpoderNome, dto.Descricao);

            await _repository.AddSuperpowerAsync(newSuperpower);
            dto.Id = newSuperpower.Id;
            return dto.ToResponse();
        }
    }
}
