using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.Heroes;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.Heroes
{
    public class UpdateHeroHandler : IUpdateHeroHandler
    {
        private readonly IHeroSuperpowerRepository _heroSuperpowerRepository;
        private readonly IHeroRepository _heroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHeroHandler(IHeroSuperpowerRepository heroSuperpowerRepository, IHeroRepository heroRepository, IUnitOfWork unitOfWork)
        {
            _heroSuperpowerRepository = heroSuperpowerRepository;
            _heroRepository = heroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HeroResponse> Handle(HeroDTO dto, int heroId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                Heroi hero = await _heroRepository.GetHeroByIdAsync(heroId)
                    ?? throw new NotFoundException("Herói não encontrado.");

                if (hero.NomeHeroi != dto.NomeHeroi)
                {
                    var heroFromDB = await _heroRepository.GetHeroByHeroName(dto.NomeHeroi);
                    if (heroFromDB != null) throw new ConflictException("Este nome de Herói já está em uso.");
                }


                hero.Nome = dto.Nome;
                hero.NomeHeroi = dto.NomeHeroi;
                hero.DataNascimento = dto.DataNascimento;
                hero.Altura = dto.Altura;
                hero.Peso = dto.Peso;

                List<int> currentSuperpowersIds = hero.HeroisSuperpoderes.Select(x => x.Superpoderes.Id).ToList();
                List<int> newSuperpowersIds = dto.Superpowers.Select(sp => sp.Id).ToList();

                if (!new HashSet<int>(currentSuperpowersIds).SetEquals(newSuperpowersIds))
                {
                    List<HeroiSuperpoder> heroSuperpowersFromDb = await _heroSuperpowerRepository.GetHeroSuperpowersByHeroId(heroId);
                    _heroSuperpowerRepository.RemoveHeroSuperpowersByIdsWithoutSaveChanges(heroSuperpowersFromDb);

                    List<HeroiSuperpoder> heroSuperpowers = dto.Superpowers
                                                             .Select(sp => new HeroiSuperpoder(heroId, sp.Id))
                                                             .ToList();

                    await _heroSuperpowerRepository.AddHeroSuperpowerAsyncWithoutSaveChanges(heroSuperpowers);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.Commit();


                hero = await _heroRepository.GetHeroByIdAsync(heroId);

                return hero.ToResponse();
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
