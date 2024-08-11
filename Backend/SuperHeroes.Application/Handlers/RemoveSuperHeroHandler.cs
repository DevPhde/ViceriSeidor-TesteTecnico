using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class RemoveSuperHeroHandler : IRemoveSuperHeroHandler
    {
        private readonly ISuperHeroRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveSuperHeroHandler(ISuperHeroRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task handle(int superHeroId)
        {
            using var transation = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var hero = await _repository.GetSuperHeroByIdAsync(superHeroId)
                ?? throw new NotFoundException("Super Herói não encontrado.");

                _repository.RemoveHeroWithoutSaveChanges(hero);

                List<int> heroSuperpowersIds = hero.HeroisSuperpoderes.Select(hs => hs.Superpoderes.Id).ToList();

                await _repository.RemoveHeroSuperpowersWithoutSaveChanges(heroSuperpowersIds, superHeroId);

                await _unitOfWork.CompleteAsync();
                await transation.CommitAsync();
            }
            catch (Exception)
            {
                await transation.RollbackAsync();
                _unitOfWork.Dispose();
                throw;
            }



        }
    }
}
