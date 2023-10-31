using TechChallenge2.Identity.Configurations;
using TechChallenge2.Identity.Interfaces.Services;
using TechChallenge2.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualBasic;
using System.Net.Mail;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using TechChallenge2.Identity.Data.Dtos;
using TechChallenge2.Identity.Interfaces;

namespace TechChallenge2.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private ITokenService _tokenService;

        public IdentityService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        public async Task SignUp(SignUpDto dto)
        {
            Usuario user = _mapper.Map<Usuario>(dto);
            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário!");
            }
        }

        public async Task<string> Login(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao autenticar usuário!");
            }

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);
            return token;
        }
    }
}
