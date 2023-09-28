using Autofac;
using BookBinder.Application.Data.Repositories;
using BookBinder.Application.Models;

namespace BookBinder.Application.Services.AuthorFeatures
{
    public class AuthorService
    {
        
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

        public async Task UpdateAuthor(FetchAuthorDto authorDto)
        {
            var author = await Repository.FetchByIdAsync(authorDto.Id); 
            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.Email = authorDto.Email;
            await Repository.UpdateAsync(author);
        }

        public async Task RemoveAuthor(Guid authorId)
        {
            var author = await Repository.FetchByIdAsync(authorId);
            await Repository.RemoveAsync(author);
        }

        public async Task<IEnumerable<FetchAuthorDto>> FetchAuthors()
        {
            var authors = await Repository.FetchAllAsync();

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
