using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Identity.Models;

namespace TechChallenge2.Identity.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
    }
}
