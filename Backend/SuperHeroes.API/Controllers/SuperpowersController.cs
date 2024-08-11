using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroes.Application.Exceptions;
using SuperHeroes.Application.Interfaces;
using SuperHeroes.Application.Mapping;
using SuperHeroes.Application.RequestModels;
using SuperHeroes.Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperpowersController : ControllerBase
    {

        [HttpGet]
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
        public async Task<ActionResult<SuperpowerResponse>> AddSuperpower([FromServices] ICreateSuperpowerHandler _handler, [FromBody] SuperpowerRequest request)
        {
            try
            {
                var newSuperpower = await _handler.Handle(request.ToDto());

                return StatusCode(201, newSuperpower);
            }
            catch (ConflictException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }

        [HttpDelete("{superpowerId}")]
        public async Task<ActionResult> RemoveSuperpowerAsync([FromServices] IRemoveSuperpowerHandler _handler, [FromRoute] int superpowerId)
        {
            try
            {
                await _handler.Handle(superpowerId);
                return Ok(new { message = "Superpoder Removido com sucesso!" });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor. Tente novamente mais tarde." });
            }
        }
    }
}