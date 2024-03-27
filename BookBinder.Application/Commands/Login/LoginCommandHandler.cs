using BookBinder.Infrastructure.Repositories.Interfaces;
using BookBinder.Infrastructure.Security;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.Login
{
    public class LoginCommandHandler : CommandHandler<LoginCommand, string>
    {
        public async override Task<ResponseResult<string>> ResolveAsync(LoginCommand command)
        {
            var user = await DbContext.AuthorRepository.FetchbyEmailAsync(command.Email);
            if(user == null)
                return ResponseResult<string>.Failed(System.Net.HttpStatusCode.NotFound);

            var passwordVerified = PasswordManager.VerifyPassword(user.PasswordHash, command.Password);
            if (!passwordVerified)
                return ResponseResult<string>.Failed(System.Net.HttpStatusCode.NotFound);

            var accessToken = TokenProvider.GetAccessToken(user.Email, user.Id);
            return ResponseResult<string>.Success(accessToken);
        }

        public IJWTProvider TokenProvider { get; set; }
        public PasswordManager PasswordManager { get; set; }
    }
}
