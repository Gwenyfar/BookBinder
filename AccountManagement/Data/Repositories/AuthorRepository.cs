using BookBinder.Application.Models;
using BookBinder.Infrastructure.Repository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Data.Repositories
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(ISession session) : base(session)
        {
        }

        
    }
}
