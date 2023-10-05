using BookBinder.Domain.Models;
using BookBinder.Infrastructure.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace BookBinder.Infrastructure.Repositories
{
    public class AuthorRepository : Repository, IAuthorRepository
    {
        public AuthorRepository(ISession session) : base(session)
        {
        }

        public async Task AddAsync(Author entity)
        {
            Session.Save(entity);
            await CommitAsync();
        }

        public async Task<List<Author>> FetchAllAsync()
        {
            var entities = await Session.Query<Author>().ToListAsync();
            return entities;
        }

        public async Task<Author> FetchByIdAsync(Guid id)
        {
            var entity = await Session.Query<Author>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task RemoveAsync(Author entity)
        {
            Session.Delete(entity);
            await CommitAsync();
        }

        public async Task UpdateAsync(Author entity)
        {
            Session.Update(entity);
            await CommitAsync();
        }
    }
}
