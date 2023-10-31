using Microsoft.IdentityModel.Tokens;

namespace TechChallenge2.Identity.Configurations;

public class JwtConfig
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public SigningCredentials SigningCredentials { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
}