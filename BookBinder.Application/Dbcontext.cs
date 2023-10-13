using BookBinder.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application
{
    /// <summary>
    /// encapsulates the repositories
    /// </summary>
    public class Dbcontext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="authorRepository">author repository</param>
        /// <param name="publisherRepository">publisher repository</param>
        public Dbcontext(IAuthorRepository authorRepository, IPublisherRepository publisherRepository)
        {
            AuthorRepository = authorRepository;
            PublisherRepository = publisherRepository;
        }
        /// <summary>
        /// author repo
        /// </summary>
        public IAuthorRepository AuthorRepository { get; set; }
        /// <summary>
        /// publisher repo
        /// </summary>
        public IPublisherRepository PublisherRepository { get; set; }
    }
}
