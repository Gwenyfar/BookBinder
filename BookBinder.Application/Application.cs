using Autofac;
using BookBinder.Application.Commands;
using BookBinder.Application.Queries;
using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application
{
    public class Application : IApplication
    {
        public async Task<ResponseResult<TResponse>> ExecuteCommandAsync<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>
        {
            var validation = command.ValidateBody();
            if (validation.NotSuccessful)
                return ResponseResult<TResponse>.Failed(HttpStatusCode.BadRequest)
                                                .AddErrors(validation.Errors);

            using var scope = Container.BeginLifetimeScope();
            var service = scope.Resolve<ICommandHandler<TCommand, TResponse>>();
            try
            {
                return await service.ResolveAsync(command);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return ResponseResult<TResponse>.Failed(HttpStatusCode.InternalServerError).AddError(ex.Message);
            }
            
        }

        public async Task<ResponseResult> ExecuteCommandAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var validation = command.ValidateBody();
            if (validation.NotSuccessful)
                return ResponseResult.Failed(HttpStatusCode.BadRequest)
                                                .AddErrors(validation.Errors);

            using var scope = Container.BeginLifetimeScope();
            var service = scope.Resolve<ICommandHandler<TCommand>>();
            try
            {
                return await service.ResolveAsync(command);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return ResponseResult.Failed(HttpStatusCode.InternalServerError).AddError(ex.Message);
            }
        }

        public async Task<ResponseResult<TResponse>> ExecuteQueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var validation = query.ValidateBody();
            if (validation.NotSuccessful)
                return ResponseResult<TResponse>.Failed(HttpStatusCode.BadRequest)
                                                .AddErrors(validation.Errors);

            using var scope = Container.BeginLifetimeScope();
            
            try
            {
                var service = scope.Resolve<IQueryHandler<TQuery, TResponse>>();
                return await service.ResolveAsync(query);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return ResponseResult<TResponse>.Failed(HttpStatusCode.InternalServerError).AddError(ex.Message);
            }
        }

        public IContainer Container { get; set; }
    }
}
