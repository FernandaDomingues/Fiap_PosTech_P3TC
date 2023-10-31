using Microsoft.EntityFrameworkCore;
using TechChallenge2.Data.Context;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Interfaces.Repositories;

namespace TechChallenge2.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Get(int id)
        {
            var obj = await _context.Set<T>()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToListAsync();
            return obj.FirstOrDefault();
        }

        public virtual async Task<List<T>> Get()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<bool> Remove(int id)
        {
            var obj = await Get(id);
            if (obj == null)
            {
                return false;
            }   
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<T> Update(T obj)
        {
            var objValid = await Get(obj.Id);
            if (objValid != null)
            {
                _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return obj;
            }

            return objValid;
        }
    }
}