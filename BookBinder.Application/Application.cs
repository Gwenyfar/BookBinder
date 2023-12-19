using Autofac;
using BookBinder.Application.Commands;
using BookBinder.Application.Queries;
using BookBinder.Infrastructure.Utilities;
using System.Diagnostics;
using System.Net;

namespace BookBinder.Application
{
    /// <summary>
    /// calls methods that handle commands and queries
    /// </summary>
    public class Application : IApplication
    {
        /// <summary>
        /// handles a command that returns data
        /// </summary>
        /// <typeparam name="TCommand">command type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="command">a command</param>
        /// <returns>a response result</returns>
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

        /// <summary>
        /// executes a command that doesn't return any data
        /// </summary>
        /// <typeparam name="TCommand">a command type</typeparam>
        /// <param name="command">a command</param>
        /// <returns>a response result</returns>
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

        /// <summary>
        /// executes a query
        /// </summary>
        /// <typeparam name="TQuery">query type</typeparam>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="query">a query</param>
        /// <returns>a response result</returns>
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

        /// <summary>
        /// autofac container
        /// </summary>
        public IContainer Container { get; set; }
    }
}
