using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Response;

namespace TechChallenge2.Domain.Interfaces.Repositories
{
    public interface INoticiaRepository : IBaseRepository<Noticia>
    {
        Task<Noticia> GetById(int id);
    }
}