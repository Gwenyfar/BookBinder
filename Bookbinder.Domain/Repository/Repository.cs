using BookBinder.Infrastructure.Models;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Repositories
{
    public class Repository<T> : IRespository where T : IEntity
    {
        public Repository(ISession session)
        {
            Session = session;
        }

        public async Task AddAsync(T entity)
        {
           Session.Save(entity);
            await CommitAsync();
        }

        public async Task<List<T>> FetchAllAsync()
        {
            var entities = await Session.Query<T>().ToListAsync();
            return entities;
        }

        public async Task<T> FetchByIdAsync(Guid id)
        {
            var entity = await Session.Query<T>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            Session.Delete(entity);
            await CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Session.Update(entity);
            await CommitAsync();
        }
        public async Task CommitAsync()
        {
            using var transaction = Session.BeginTransaction();
            try
            {
                transaction.Commit();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public ISession Session { get; set; }
    }
}
