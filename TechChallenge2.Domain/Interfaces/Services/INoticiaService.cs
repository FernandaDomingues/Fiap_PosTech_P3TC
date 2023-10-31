using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Response;

namespace TechChallenge2.Domain.Interfaces.Services
{
    public interface INoticiaService
    {
        Task<Noticia> Create(Noticia noticiaDTO);
        Task<BaseResponse> Update(Noticia noticiaDTO);
        Task<BaseResponse> Remove(int id);
        Task<Noticia> Get(int id);
        Task<List<Noticia>> Get();
        Task<BaseResponse> GetById(int id);
    }
}
