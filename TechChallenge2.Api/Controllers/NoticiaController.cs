using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Request;
using TechChallenge2.Domain.Entities.Response;
using TechChallenge2.Domain.Exceptions;
using TechChallenge2.Domain.Interfaces.Services;

namespace TechChallenge2.Api.Controllers
{
    [ApiController]
    [Route("api/v1/noticia")]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaService _noticiaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controle responsável pela noticia
        /// </summary>
        /// <param name="noticiaService"></param>
        /// <param name="mapper"></param>
        public NoticiaController(INoticiaService noticiaService, IMapper mapper)
        {
            _noticiaService = noticiaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint responsável por cadastrar nova noticia
        /// </summary>
        /// <param name="noticiaRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] NoticiaRequest noticiaRequest)
        {
            try
            {
                var noticia = _mapper.Map<Noticia>(noticiaRequest);
                var noticiaCreated = await _noticiaService.Create(noticia);

                return Ok(new BaseResponse
                {
                    Message = "Solicitação cadastrada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = noticiaCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por alterar noticia
        /// </summary>
        /// <param name="noticiaRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<ActionResult> Update([Required] int id,[FromBody] NoticiaRequest noticiaRequest)
        {
            try
            {
                var noticia = _mapper.Map<Noticia>(noticiaRequest);
                noticia.Id = id;
                var response = await _noticiaService.Update(noticia);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);

            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por buscar todas as noticias
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaNoticiaS = await _noticiaService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Notícias resgatadas com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = listaNoticiaS
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por buscar noticia pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getById/{id}")]
        [Authorize]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {

                var response = await _noticiaService.GetById(id);

                if (response.Success)
                    return Ok(response);

                return BadRequest(response);
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por deletar noticia pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("remove/{id}")]
        [Authorize]
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                var response = await _noticiaService.Remove(id);
                if (response.Success)
                    return Ok(response);

                return BadRequest(response);

            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }
    }
}
