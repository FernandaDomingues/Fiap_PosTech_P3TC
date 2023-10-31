

using TechChallenge2.Identity.Data.Dtos;

namespace TechChallenge2.Identity.Interfaces.Services;

public interface IIdentityService
{
    Task SignUp(SignUpDto dto);
    Task<string> Login(LoginDto dto);
}