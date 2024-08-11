using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class RemoveSuperpowerHandler : IRemoveSuperpowerHandler
    {
        private readonly ISuperHeroRepository _repository;

        public RemoveSuperpowerHandler(ISuperHeroRepository repository) => _repository = repository;

        public async Task Handle(int superPowerId)
        {
            Superpoder superpower = await _repository.GetSuperpowerByIdAsync(superPowerId) ?? throw new NotFoundException("Superpoder não encontrado.");

            await _repository.RemoveSuperpowerAsync(superpower);
        }
    }
}
