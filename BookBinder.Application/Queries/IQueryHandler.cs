using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries
{
    /// <summary>
    /// queryhandler interface 
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        /// <summary>
        /// handles the query request
        /// </summary>
        /// <param name="query">request body</param>
        /// <returns>response result of a type</returns>
        Task<ResponseResult<TResponse>> ResolveAsync(TQuery query);

        /// <summary>
        /// contains the repositories
        /// </summary>
        Dbcontext DbContext { get; set; }
    }

    /// <summary>
    /// abstract implementation of queryhandler
    /// </summary>
    /// <typeparam name="TQuery">query type</typeparam>
    /// <typeparam name="TResponse">response type</typeparam>
    public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        /// <summary>
        /// handles a query
        /// </summary>
        /// <param name="query">the request body</param>
        /// <returns>response result with some data</returns>
        public abstract Task<ResponseResult<TResponse>> ResolveAsync(TQuery query);
        /// <summary>
        /// contains repositories
        /// </summary>
        public Dbcontext DbContext { get; set; }
    }
}
