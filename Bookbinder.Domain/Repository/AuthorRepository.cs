using BookBinder.Application.Models;
using NHibernate;

namespace BookBinder.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(ISession session) : base(session)
        {
        }

        
    }
}
