using Microsoft.AspNetCore.Mvc;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces.Heroes;
using SuperHeroes.Application.Interfaces.SuperHeroes;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.API.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {

        [HttpGet]
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
        public async Task<ActionResult<HeroResponse>> GetHeroById([FromServices] IGetHeroByIdHandler _handler, [FromRoute] int heroId)
        {
            try
            {
                var hero = await _handler.Handle(heroId);
                return Ok(hero);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<HeroResponse>> AddHero([FromServices] ICreateHeroHandler _handler, [FromBody] HeroRequest request)
        {
            try
            {
                var newHero = await _handler.Handle(request.ToDto());

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
        public async Task<ActionResult<HeroResponse>> UpdateHero([FromServices] IUpdateHeroHandler _handler, [FromRoute] int HeroId, [FromBody] HeroRequest request)
        {
            try
            {
                var updatedHero = await _handler.Handle(request.ToDto(), HeroId);

                return Ok(updatedHero);
            }
            catch (ConflictException ex)
            {
                return Conflict(new { ex.Message });
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

        [HttpDelete("{heroId}")]
        public async Task<ActionResult> DeleteHero([FromServices] IRemoveHeroHandler _handler, [FromRoute] int heroId)
        {
            try
            {
                await _handler.Handle(heroId);
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
