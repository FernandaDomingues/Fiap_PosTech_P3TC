using Microsoft.AspNetCore.Identity;

namespace TechChallenge2.Identity.Models
{
    public class Usuario : IdentityUser
    {

        public Usuario() : base() { }
        public DateTime DateBirth { get; set; }
    }
}
