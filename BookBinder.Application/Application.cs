using Autofac;
using BookBinder.Application.Commands;
using BookBinder.Application.Queries;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application
{
    public class Application : IApplication
    {
        public Task<ResponseResult<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult> ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : Command
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            throw new NotImplementedException();
        }

        public IContainer Container { get; set; }
    }
}
