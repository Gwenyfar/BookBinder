using BookBinder.Domain.DTOs;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries.FetchAuthors
{
    /// <summary>
    /// request to fetch all authors
    /// </summary>
    public class FetchAuthorsQuery : IQuery<IEnumerable<FetchAuthorDto>>
    {
        /// <summary>
        /// validates request body
        /// </summary>
        /// <returns>successful response result</returns>
        public ResponseResult ValidateBody()
        {
            return ResponseResult.Success();
        }
    }
}
