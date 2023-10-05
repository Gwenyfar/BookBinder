using BookBinder.Domain.Models;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : CommandHandler<CreateAuthorCommand, Guid>
    {
        public override async Task<ResponseResult<Guid>> ResolveAsync(CreateAuthorCommand command)
        {
            var newAuthor = new Author
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
            };
            await DbContext.AuthorRepository.AddAsync(newAuthor);
            return ResponseResult<Guid>.Success(newAuthor.Id);
        }
    }
}
