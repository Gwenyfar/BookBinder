using BookBinder.Domain.DTOs;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries.FetchAuthors
{
    public class FetchAuthorsQueryHandler : QueryHandler<FetchAuthorsQuery, IEnumerable<FetchAuthorDto>>
    {
        public override async Task<ResponseResult<IEnumerable<FetchAuthorDto>>> ResolveAsync(FetchAuthorsQuery query)
        {
            var authors = await DbContext.AuthorRepository.FetchAllAsync();
            var authorDtos = authors.Select(a => new FetchAuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email
            });
            return ResponseResult<IEnumerable<FetchAuthorDto>>.Success(authorDtos);
        }
    }
}
