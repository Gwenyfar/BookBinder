using BookBinder.Infrastructure.Security;
using BookBinder.Infrastructure.Utilities;
using System.Security.Claims;

namespace BookBinder.Application.Commands.LogIn
{
    public class LoginCommandHandler : CommandHandler<LoginCommand, ClaimsPrincipal>
    {
        public override async Task<ResponseResult<ClaimsPrincipal>> ResolveAsync(LoginCommand command)
        {
            var response = await AccessManager.AuthenticateAsync(command.Email, command.Password);
            var principal = AccessManager.TokenProvider.CreateClaimsIdentity(response.Response);
            return ResponseResult<ClaimsPrincipal>.Success(principal);
        }
        public IAccessManager AccessManager { get; set; }
    }
}
