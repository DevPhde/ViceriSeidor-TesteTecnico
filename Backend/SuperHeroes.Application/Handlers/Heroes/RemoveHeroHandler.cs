using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.Heroes;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using SuperHeroes.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.Heroes
{
    public class RemoveHeroHandler : IRemoveHeroHandler
    {
        private readonly IHeroSuperpowerRepository _heroSuperpowerRepository;
        private readonly IHeroRepository _heroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveHeroHandler(IHeroSuperpowerRepository heroSuperpowerRepository, IUnitOfWork unitOfWork, IHeroRepository heroRepository)
        {
            _heroSuperpowerRepository = heroSuperpowerRepository;
            _unitOfWork = unitOfWork;
            _heroRepository = heroRepository;
        }

        public async Task Handle(int heroId)
        {
            try
            {
                var hero = await _heroRepository.GetHeroByIdAsync(heroId)
                ?? throw new NotFoundException("Herói não encontrado.");

                await _unitOfWork.BeginTransactionAsync();

                _heroRepository.RemoveHeroWithoutSaveChanges(hero);

                List<int> heroSuperpowersIds = hero.HeroisSuperpoderes.Select(hs => hs.Superpoderes.Id).ToList();

                List<HeroiSuperpoder> heroSuperpowers = await _heroSuperpowerRepository.GetHeroSuperpowersByHeroId(heroId);

                _heroSuperpowerRepository.RemoveHeroSuperpowersByIdsWithoutSaveChanges(heroSuperpowers);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();

                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }



        }
    }
}
