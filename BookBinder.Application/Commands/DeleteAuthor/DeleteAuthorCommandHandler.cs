using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : CommandHandler<DeleteAuthorCommand>
    {
        public override async Task<ResponseResult> ResolveAsync(DeleteAuthorCommand command)
        {
            var author = await DbContext.AuthorRepository.FetchByIdAsync(command.Id);
            if(author == null)
            {
                return ResponseResult.Failed(System.Net.HttpStatusCode.NotFound).AddError("author doesn't exist");
            }

            await DbContext.AuthorRepository.RemoveAsync(author);
            return ResponseResult.Success();
        }
    }
}
