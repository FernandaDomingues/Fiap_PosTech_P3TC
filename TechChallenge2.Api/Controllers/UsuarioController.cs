using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TechChallenge2.Identity.Data.Dtos;
using TechChallenge2.Identity.Interfaces.Services;

namespace TechChallenge2.Api.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private IIdentityService _service;

        private readonly IBus _bus;

        private readonly IConfiguration _config;

        public UsuarioController(IIdentityService service, IBus bus, IConfiguration config)
        {
            _service = service;
            _bus = bus;
            _config = config;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> CreateUser(SignUpDto dto)
        {
            await _service.SignUp(dto);
            try
            {
                var fila = _config.GetSection("AzureServiceBus")["NomeFila"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{fila}"));
                await endpoint.Send(dto);
            }
            catch
            {
                throw new ApplicationException("Falha ao enviar e-mail de confirmação!");
            }
            
            return Ok("Usuário cadastrado");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.Login(dto);
            return Ok(token);
        }
    }
}
