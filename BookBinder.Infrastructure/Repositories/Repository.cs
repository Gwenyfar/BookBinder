using BookBinder.Domain.Models;
using NHibernate;
using NHibernate.Linq;

namespace BookBinder.Infrastructure.Repositories
{
    public class Repository 
    {
        public Repository(ISession session)
        {
            Session = session;
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
