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

        public UsuarioController(IIdentityService service)
        {
            _service = service;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> CreateUser(SignUpDto dto)
        {
            await _service.SignUp(dto);
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
