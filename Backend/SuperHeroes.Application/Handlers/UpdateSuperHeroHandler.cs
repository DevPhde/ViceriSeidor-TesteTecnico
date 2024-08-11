using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class UpdateSuperHeroHandler : IUpdateSuperHeroHandler
    {
        private readonly ISuperHeroRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSuperHeroHandler(ISuperHeroRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HeroAndSuperpowersResponse> Handle(HeroAndSuperpowersDTO dto, int superHeroId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                Heroi hero = await _repository.GetSuperHeroByIdAsync(superHeroId)
                    ?? throw new NotFoundException("Herói não encontrado.");

                if (hero.NomeHeroi != dto.NomeHeroi)
                {
                    var heroByNameFromDB = await _repository.GetHeroByHeroName(dto.NomeHeroi);
                    if (heroByNameFromDB != null) throw new ConflictException("Este nome de super Herói já está em uso.");
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
                    await _repository.RemoveHeroSuperpowersWithoutSaveChanges(currentSuperpowersIds, superHeroId);

                    List<HeroiSuperpoder> heroSuperpowers = dto.Superpowers
                                                             .Select(sp => new HeroiSuperpoder(superHeroId, sp.Id))
                                                             .ToList();

                    await _repository.AddListOfSuperpowersWithoutSaveChanges(heroSuperpowers);
                }

                await transaction.CommitAsync();
                await _unitOfWork.CompleteAsync();


                hero = await _repository.GetSuperHeroByIdAsync(superHeroId);

                return hero.ToResponse();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                _unitOfWork.Dispose();
                throw;
            }
        }
    }
}
