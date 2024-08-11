using SuperHeroes.Application.DTOs;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.ResponseModels;
using SuperHeroes.Domain.Entities;
using SuperHeroes.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroes.Application.Handlers
{
    public class CreateSuperHeroHandler : ICreateSuperHeroHandler
    {
        private readonly ISuperHeroRepository _repository;

        public CreateSuperHeroHandler(ISuperHeroRepository repository) => _repository = repository;

        public async Task<HeroAndSuperpowersDTO> Handle(HeroAndSuperpowersDTO dto)
        {
            Heroi createdHero = new Heroi();
            try
            {

                var hero = await _repository.GetHeroByHeroName(dto.NomeHeroi);
                if (hero != null)
                {
                    throw new ConflictException("Nome de Heroi já cadastrado.");
                }

                createdHero = await _repository.AddHeroAsync(new Heroi(dto.Nome, dto.NomeHeroi, dto.DataNascimento, dto.Altura, dto.Peso));
                dto.Id = createdHero.Id;
                List<HeroiSuperpoder> heroSuperpowers = dto.Superpowers.Select(sp => new HeroiSuperpoder(createdHero.Id, sp.Id)).ToList();

                await _repository.AddListOfSuperpowersAsyncTransaction(heroSuperpowers);

                return dto;

            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                if (createdHero.Id != 0)
                {
                    await _repository.RollbackAddedHeroAsync(createdHero.Id);
                }
                throw;
            }
        }
    }
}
