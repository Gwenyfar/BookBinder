using BookBinder.Application.Models;
using NHibernate;

namespace BookBinder.Infrastructure.Repositories
{
    public class PublisherRepository : Repository<Publisher>
    {
        public PublisherRepository(ISession session) : base(session)
        {
        }
    }
}
