using Autofac;
using BookBinder.Application.Data.Repositories;
using BookBinder.Application.Models;

namespace BookBinder.Application.Services.AuthorFeatures
{
    public class AuthorService : Application
    {
        public AuthorService()
        {
            var scope = Container.BeginLifetimeScope();
            Repository = scope.Resolve<AuthorRepository>();
        }

        public async Task<Guid> AddAuthor(AuthorDto author)
        {
            var newAuthor = new Author
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
            };
           await Repository.AddAsync(newAuthor);
            return newAuthor.Id;
        }

        public async Task<IEnumerable<FetchAuthorDto>> FetchAuthors()
        {
            var authors = Repository.FetchAllAsync();

            return authors.Select(a => new FetchAuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email
            });
        }

        public AuthorRepository Repository { get; set; }
    }
}
