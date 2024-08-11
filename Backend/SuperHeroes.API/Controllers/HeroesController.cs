using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Interfaces.Heroes;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.API.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroesHub _heroesHub;

        public HeroesController(IHeroesHub heroesHub) => _heroesHub = heroesHub;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<HeroResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Retorna todos os heróis com seus superpoderes", Description = "Este endpoint retorna uma lista de heróis com detalhes de seus superpoderes")]
        [SwaggerResponse(200, "Retornada lista com todos os heróis cadastrados.", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Tente novamente mais tarde.", typeof(string))]
        public async Task<ActionResult<List<HeroResponse>>> GetAllHeroes([FromServices] IGetAllHeroesHandler _handler)
        {
            try
            {
                var heroesWithSuperpowers = await _handler.Handle();
                return Ok(heroesWithSuperpowers);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{heroId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HeroResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Busca um herói pelo ID", Description = "Este endpoint retorna os detalhes de um herói com base no ID fornecido")]
        [SwaggerResponse(201, "O herói foi criado com sucesso.", typeof(HeroResponse))]
        [SwaggerResponse(404, "Conflito ao criar o herói. Exemplo de retorno: 'Herói não encontrado.'", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Tente novamente mais tarde.", typeof(string))]
        public async Task<ActionResult<HeroResponse>> GetHeroById([FromServices] IGetHeroByIdHandler _handler, [FromRoute] int heroId)
        {
            try
            {
                var hero = await _handler.Handle(heroId);
                return Ok(hero);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HeroResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Adiciona um novo herói", Description = "Este endpoint permite a criação de um novo herói com superpoderes")]
        [SwaggerResponse(201, "O herói foi criado com sucesso.", typeof(HeroResponse))]
        [SwaggerResponse(409, "Conflito ao criar o herói. Exemplo de retorno: 'Nome de Heroi já cadastrado.'", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Tente novamente mais tarde.", typeof(string))]
        public async Task<ActionResult<HeroResponse>> AddHero([FromServices] ICreateHeroHandler _handler, [FromBody] HeroRequest request)
        {
            try
            {
                var newHero = await _handler.Handle(request.ToDto());
                await _heroesHub.SendHeroes();
                return CreatedAtAction(nameof(GetHeroById), new { heroId = newHero.Id }, newHero);
            }
            catch (ConflictException ex)
            {
                return Conflict(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPut("{heroId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HeroResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]  
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Atualiza um herói existente", Description = "Este endpoint permite a atualização dos dados de um herói existente")]
        [SwaggerResponse(200, "Herói atualizado com sucesso.", typeof(HeroResponse))]
        [SwaggerResponse(404, "Herói não encontrado. Exemplo de retorno: 'Herói não encontrado.'", typeof(string))]
        [SwaggerResponse(409, "Conflito ao atualizar o herói. Exemplo de retorno: 'Este nome de Herói já está em uso.'", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Tente novamente mais tarde.", typeof(string))]
        public async Task<ActionResult<HeroResponse>> UpdateHero([FromServices] IUpdateHeroHandler _handler, [FromRoute] int HeroId, [FromBody] HeroRequest request)
        {
            try
            {
                var updatedHero = await _handler.Handle(request.ToDto(), HeroId);
                await _heroesHub.SendHeroes();
                return Ok(updatedHero);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (ConflictException ex)
            {
                return Conflict(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{heroId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Deleta um herói", Description = "Este endpoint permite a remoção de um herói existente com base no ID")]
        [SwaggerResponse(200, "Herói removido com sucesso.", typeof(string))]
        [SwaggerResponse(404, "Herói não encontrado. Exemplo de retorno: 'Herói não encontrado.'", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Tente novamente mais tarde.", typeof(string))]
        public async Task<ActionResult> DeleteHero([FromServices] IRemoveHeroHandler _handler, [FromRoute] int heroId)
        {
            try
            {
                await _handler.Handle(heroId);
                await _heroesHub.SendHeroes();
                return Ok(new { message = "Herói removido com sucesso!" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }
    }
}
