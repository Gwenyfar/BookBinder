using BookBinder.Domain.DTOs;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries.FetchAuthors
{
    public class FetchAuthorsQuery : IQuery<IEnumerable<FetchAuthorDto>>
    {
        public ResponseResult ValidateBody()
        {
            return ResponseResult.Success();
        }
    }
}
