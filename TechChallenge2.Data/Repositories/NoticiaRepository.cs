using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Data.Context;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Interfaces.Repositories;

namespace TechChallenge2.Data.Repositories
{
    public class NoticiaRepository : BaseRepository<Noticia>, INoticiaRepository
    {
        private readonly DataContext _context;

        public NoticiaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Noticia> GetById(int id)
        {
            var query = from noticia in _context.Noticias
                        where noticia.Id == id
                        select noticia;

            return await query.FirstOrDefaultAsync();
        }
    }
}
