using BookBinder.Infrastructure.Models;
using NHibernate;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Repository
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

        public List<T> FetchAllAsync()
        {
            var entities = Session.Query<T>().ToList();
            return entities;
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
