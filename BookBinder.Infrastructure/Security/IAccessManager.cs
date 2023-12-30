using Bookbinder.Domain.Models;
using BookBinder.Infrastructure.Repositories.Interfaces;
using BookBinder.Infrastructure.Utilities;

namespace BookBinder.Infrastructure.Security
{
    public interface IAccessManager
    {
        Task<ResponseResult<ApplicationUser>> AuthenticateAsync(string email, string password);
        public IAuthorRepository AuthorRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        public ITokenProvider TokenProvider { get; }
        public IPasswordManager PasswordManager { get; }
    }
}
