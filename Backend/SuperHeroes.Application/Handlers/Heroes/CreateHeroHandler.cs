using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers.SuperHeroes
{
    public class CreateHeroHandler : ICreateHeroHandler
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IHeroSuperpowerRepository _heroSuperpowerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateHeroHandler(IHeroRepository heroRepository, IHeroSuperpowerRepository superHeroRepository, IUnitOfWork unitOfWork)
        {
            _heroRepository = heroRepository;
            _heroSuperpowerRepository = superHeroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<HeroDTO> Handle(HeroDTO dto)
        {
            try
            {


                var hero = await _heroRepository.GetHeroByHeroName(dto.NomeHeroi);
                if (hero != null)
                {
                    throw new ConflictException("Nome de Heroi já cadastrado.");
                }

                await _unitOfWork.BeginTransactionAsync();

                Heroi createdHero = await _heroRepository.AddHeroAsyncWithoutSaveChanges(new Heroi(dto.Nome, dto.NomeHeroi, dto.DataNascimento, dto.Altura, dto.Peso));
                await _unitOfWork.SaveChangesAsync();

                dto.Id = createdHero.Id;

                List<HeroiSuperpoder> heroSuperpowers = dto.Superpowers.Select(sp => new HeroiSuperpoder(createdHero.Id, sp.Id)).ToList();

                await _heroSuperpowerRepository.AddHeroSuperpowerAsyncWithoutSaveChanges(heroSuperpowers);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.Commit();

                return dto;

            }
            catch (BadRequestException)
            {
                await _unitOfWork.Rollback();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork?.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
