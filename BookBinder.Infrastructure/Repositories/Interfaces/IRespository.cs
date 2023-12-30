using BookBinder.Domain.Models;

namespace BookBinder.Infrastructure.Repositories.Interfaces
{
    public interface IRespository<T> where T : IEntity
    {
        Task AddAsync(T entity);
        Task<List<T>> FetchAllAsync();
        Task<T> FetchByIdAsync(Guid id);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> ExistsAsync(string name);
    }
}
