using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries
{
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<ResponseResult<TResponse>> ResolveAsync(TQuery query);
        Dbcontext DbContext { get; set; }
    }

    public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        public abstract Task<ResponseResult<TResponse>> ResolveAsync(TQuery query);
        public Dbcontext DbContext { get; set; }
    }
}
