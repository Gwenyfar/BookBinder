using Bookbinder.Domain.Models;
using BookBinder.Domain.Models;
using BookBinder.Infrastructure.Security;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.CreateAuthor
{
    /// <summary>
    /// handles the request to create an author
    /// </summary>
    public class CreateAuthorCommandHandler : CommandHandler<CreateAuthorCommand, Guid>
    {
        /// <summary>
        /// creates a new author
        /// </summary>
        /// <param name="command">details of the author</param>
        /// <returns>the unique id of new uthor</returns>
        public override async Task<ResponseResult<Guid>> ResolveAsync(CreateAuthorCommand command)
        {
            var authorExists = await AccessManager.AuthorRepository.ExistsAsync(command.Email);
            if (authorExists)
                return ResponseResult<Guid>.Failed(System.Net.HttpStatusCode.BadRequest);
            var passwordHash = AccessManager.PasswordManager.HashPassword(command.Password);
            var newAuthor = new Author
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PasswordHash = passwordHash,
                Role = AccountType.Author
            };
            await DbContext.AuthorRepository.AddAsync(newAuthor);
            return ResponseResult<Guid>.Success(newAuthor.Id);
        }
        public IAccessManager AccessManager { get; set; }
    }
}
