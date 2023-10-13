using BookBinder.Application.Commands;
using BookBinder.Application.Queries;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application
{
    /// <summary>
    /// interface for application functionalities
    /// </summary>
    public interface IApplication
    {
        Task<ResponseResult<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>;
        Task<ResponseResult> ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : Command;

        Task<ResponseResult<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}
