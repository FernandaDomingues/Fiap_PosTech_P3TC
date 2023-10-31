using TechChallenge2.Domain.Entities;

namespace TechChallenge2.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<bool> Remove(int id);
        Task<T> Get(int id);
        Task<List<T>> Get();
    }
}
