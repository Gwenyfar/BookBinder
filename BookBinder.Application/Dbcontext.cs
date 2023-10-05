using BookBinder.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application
{
    public class Dbcontext
    {
        public Dbcontext(IAuthorRepository authorRepository, IPublisherRepository publisherRepository)
        {
            AuthorRepository = authorRepository;
            PublisherRepository = publisherRepository;
        }
        public IAuthorRepository AuthorRepository { get; set; }
        public IPublisherRepository PublisherRepository { get; set; }
    }
}
