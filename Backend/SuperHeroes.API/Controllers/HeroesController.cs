using Microsoft.AspNetCore.Mvc;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace SuperHeroes.API.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<HeroAndSuperpowersResponse>>> GetallSuperHeroes([FromServices] IGetAllHeroesAndSuperpowersHandler _handler)
        {
            try
            {
                var heroesAndSuperpowers = await _handler.Handle();
                return Ok(heroesAndSuperpowers);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpGet("{superHeroId}")]
        public async Task<ActionResult<HeroAndSuperpowersResponse>> GetSuperHeroById([FromServices] IGetSuperHeroByIdHandler _handler, [FromRoute] int superHeroId)
        {
            try
            {
                var hero = await _handler.Handle(superHeroId);
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
        public async Task<ActionResult<HeroAndSuperpowersResponse>> AddNewSuperHero([FromServices] ICreateSuperHeroHandler _handler, [FromBody] SuperHeroRequest request)
        {
            try
            {
                var newSuperHero = await _handler.Handle(request.ToDto());

                return CreatedAtAction(nameof(GetSuperHeroById), new { superHeroId = newSuperHero.Id }, newSuperHero);
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

        [HttpPut("{superHeroId}")]
        public async Task<ActionResult<HeroAndSuperpowersResponse>> UpdateSuperHero([FromServices] IUpdateSuperHeroHandler _handler, [FromRoute] int superHeroId, [FromBody] SuperHeroRequest request)
        {
            try
            {
                var updatedSuperHero = await _handler.Handle(request.ToDto(), superHeroId);

                return Ok(updatedSuperHero);
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

        [HttpDelete("{superHeroId}")]
        public async Task<ActionResult> DeleteSuperHero([FromServices] IRemoveSuperHeroHandler _handler, [FromRoute] int superHeroId)
        {
            try
            {
                await _handler.handle(superHeroId);
                return Ok(new {message = "Super-Herói removido com sucesso!" });
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
