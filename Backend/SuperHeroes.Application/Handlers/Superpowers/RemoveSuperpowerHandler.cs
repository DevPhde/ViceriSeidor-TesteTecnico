using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.Superpowers;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.Superpowers
{
    public class RemoveSuperpowerHandler : IRemoveSuperpowerHandler
    {
        private readonly ISuperpowerRepository _superpowerRepository;

        public RemoveSuperpowerHandler(ISuperpowerRepository superpowerRepository) => _superpowerRepository = superpowerRepository;

        public async Task Handle(int superpowerId)
        {
            Superpoder superpower = await _superpowerRepository.GetSuperpowerByIdAsync(superpowerId) ?? throw new NotFoundException("Superpoder não encontrado.");

            await _superpowerRepository.RemoveSuperpowerAsync(superpower);
        }
    }
}
