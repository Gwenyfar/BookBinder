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
    public class PublisherRepository : Repository<Publisher>
    {
        public PublisherRepository(ISession session) : base(session)
        {
        }
    }
}
