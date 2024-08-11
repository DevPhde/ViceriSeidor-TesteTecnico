using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Interfaces.Superpowers;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperpowersController : ControllerBase
    {
        private readonly IHeroesHub _heroesHub;

        public SuperpowersController(IHeroesHub heroesHub) => _heroesHub = heroesHub;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SuperpowerResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Retorna todos os superpoderes", Description = "Este endpoint retorna uma lista de todos os superpoderes disponíveis.")]
        [SwaggerResponse(200, "Retorna com sucesso uma lista contendo os superpoderes cadastrados.", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Exemplo de retorno: 'Erro interno no servidor. Tente novamente mais tarde.'", typeof(string))]
        public async Task<ActionResult<List<SuperpowerResponse>>> GetAllSuperpowers([FromServices] IGetAllSuperpowersHandler _handler)
        {
            try
            {
                List<SuperpowerResponse> superpowers = await _handler.Handle();
                return Ok(superpowers);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuperpowerResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Adiciona um novo superpoder", Description = "Este endpoint permite a criação de um novo superpoder.")]
        [SwaggerResponse(201, "O superpoder foi criado com sucesso.", typeof(SuperpowerResponse))]
        [SwaggerResponse(409, "Conflito ao criar o superpoder. Exemplo de retorno: 'Superpoder já cadastrado com esse nome.'", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Exemplo de retorno: 'Erro interno no servidor. Tente novamente mais tarde.'", typeof(string))]
        public async Task<ActionResult<SuperpowerResponse>> AddSuperpower([FromServices] ICreateSuperpowerHandler _handler, [FromBody] SuperpowerRequest request)
        {
            try
            {
                var newSuperpower = await _handler.Handle(request.ToDto());
                await _heroesHub.SendHeroes();
                return StatusCode(201, newSuperpower);
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

        [HttpDelete("{superpowerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(object))]
        [SwaggerOperation(Summary = "Remove um superpoder", Description = "Este endpoint permite a remoção de um superpoder pelo seu ID.")]
        [SwaggerResponse(200, "Superpoder removido com sucesso.", typeof(object))]
        [SwaggerResponse(404, "Superpoder não encontrado. Exemplo de retorno: 'Superpoder não encontrado.'", typeof(string))]
        [SwaggerResponse(500, "Erro interno no servidor. Exemplo de retorno: 'Erro interno no servidor. Tente novamente mais tarde.'", typeof(string))]
        public async Task<ActionResult> RemoveSuperpowerAsync([FromServices] IRemoveSuperpowerHandler _handler, [FromRoute] int superpowerId)
        {
            try
            {
                await _handler.Handle(superpowerId);
                await _heroesHub.SendHeroes();
                return Ok(new { message = "Superpoder Removido com sucesso!" });
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