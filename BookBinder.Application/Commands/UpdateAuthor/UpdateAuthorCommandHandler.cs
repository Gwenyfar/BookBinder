using BookBinder.Domain.DTOs;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandHandler : CommandHandler<UpdateAuthorCommand>
    {
        public override async Task<ResponseResult> ResolveAsync(UpdateAuthorCommand command)
        {
            var author = await DbContext.AuthorRepository.FetchByIdAsync(command.Id);
            if (author == null)
            {
                return ResponseResult.Failed(System.Net.HttpStatusCode.NotFound).AddError("author doesn't exist");
            }

            author.FirstName = command.FirstName;
            author.LastName = command.LastName;
            author.Email = command.Email;

            await DbContext.AuthorRepository.UpdateAsync(author);
            return ResponseResult.Success();
        }
    }
}
