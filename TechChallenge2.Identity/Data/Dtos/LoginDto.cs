using System.ComponentModel.DataAnnotations;

namespace TechChallenge2.Identity.Data.Dtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
