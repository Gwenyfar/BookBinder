using Bookbinder.Domain.Models;
using BookBinder.Infrastructure.Repositories.Interfaces;
using BookBinder.Infrastructure.Utilities;

namespace BookBinder.Infrastructure.Security
{
    public class AccessManager : IAccessManager
    {
        public AccessManager(IAuthorRepository authorRepository, IPublisherRepository publisherRepository, 
            ITokenProvider tokenProvider, IPasswordManager passwordManager)
        {
            AuthorRepository = authorRepository;
            PublisherRepository = publisherRepository;
            TokenProvider = tokenProvider;
            PasswordManager = passwordManager;
        }
        public async Task<ResponseResult<ApplicationUser>> AuthenticateAsync(string email, string password)
        {
            ApplicationUser user;
            user = await AuthorRepository.FetchByEmailAsync(email);
            if (user == null)
                user = await PublisherRepository.FetchByEmailAsync(email);
            if (user == null)
                return ResponseResult<ApplicationUser>.Failed(System.Net.HttpStatusCode.NotFound);

            var passwordIsCorrect = PasswordManager.VerifyPassword(password, user.PasswordHash);
            if (!passwordIsCorrect)
                return ResponseResult<ApplicationUser>.Failed(System.Net.HttpStatusCode.NotFound);
            return ResponseResult<ApplicationUser>.Success(user);
        }

        public IAuthorRepository AuthorRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        public ITokenProvider TokenProvider { get; }
        public IPasswordManager PasswordManager { get; }

        
    }
}
