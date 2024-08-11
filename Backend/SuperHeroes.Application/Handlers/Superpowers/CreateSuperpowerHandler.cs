using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.Superpowers;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.Superpowers
{
    public class CreateSuperpowerHandler : ICreateSuperpowerHandler
    {
        private readonly ISuperpowerRepository _superpowerRepository;

        public CreateSuperpowerHandler(ISuperpowerRepository superpowerRepository) => _superpowerRepository = superpowerRepository;

        public async Task<SuperpowerResponse> Handle(SuperpowerDTO dto)
        {
            Superpoder superpower = await _superpowerRepository.GetSuperpowerByNameAsync(dto.SuperpoderNome);
            if (superpower != null)
            {
                throw new ConflictException("Superpoder já cadastrado com esse nome.");
            }
            Superpoder newSuperpower = new Superpoder(dto.SuperpoderNome, dto.Descricao);

            await _superpowerRepository.AddSuperpowerAsync(newSuperpower);
            dto.Id = newSuperpower.Id;
            return dto.ToResponse();
        }
    }
}
