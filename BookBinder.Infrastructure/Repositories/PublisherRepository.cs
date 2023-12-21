using BookBinder.Domain.Models;
using BookBinder.Infrastructure.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace BookBinder.Infrastructure.Repositories
{
    public class PublisherRepository : Repository, IPublisherRepository
    {
        public PublisherRepository(ISession session) : base(session)
        {
        }

        public async Task AddAsync(Publisher entity)
        {
            Session.Save(entity);
            await CommitAsync();
        }

        public async Task<bool> ExistsAsync(string email)
        {
            var exists = await Session.Query<Publisher>().AnyAsync(a => a.Email.ToLower() == email.ToLower());
            return exists;
        }

        public async Task<List<Publisher>> FetchAllAsync()
        {
            var entities = await Session.Query<Publisher>().ToListAsync();
            return entities;
        }

        public async Task<Publisher> FetchByEmailAsync(string email)
        {
            var publisher = await Session.Query<Publisher>().FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
            return publisher;
        }

        public async Task<Publisher> FetchByIdAsync(Guid id)
        {
            var entity = await Session.Query<Publisher>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task RemoveAsync(Publisher entity)
        {
            Session.Delete(entity);
            await CommitAsync();
        }

        public async Task UpdateAsync(Publisher entity)
        {
            Session.Update(entity);
            await CommitAsync();
        }
    }
}
