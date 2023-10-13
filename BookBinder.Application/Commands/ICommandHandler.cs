using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands
{
    /// <summary>
    /// interface for all commandhandlers returning data
    /// </summary>
    /// <typeparam name="TCommand">type of command</typeparam>
    /// <typeparam name="TResponse">type of data</typeparam>
    public interface ICommandHandler<TCommand, TResponse> where TCommand : Command<TResponse>
    {
        /// <summary>
        /// handles a command
        /// </summary>
        /// <param name="command">command to resolve</param>
        /// <returns>generic response result</returns>
       Task<ResponseResult<TResponse>> ResolveAsync(TCommand command);
       Dbcontext DbContext { get; set; }
    }
    /// <summary>
    /// interface for all commandhandlers not returning data
    /// </summary>
    /// <typeparam name="TCommand">the type of the command</typeparam>
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        /// <summary>
        /// handles a command
        /// </summary>
        /// <param name="command">type of command</param>
        /// <returns>a response result</returns>
        Task<ResponseResult> ResolveAsync(TCommand command);
    }

    /// <summary>
    /// an abstract commandhandler implementation
    /// </summary>
    /// <typeparam name="TCommand">type of command</typeparam>
    /// <typeparam name="TResponse">type of response</typeparam>
    public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>  where TCommand : Command<TResponse>
    {
        /// <summary>
        /// contains all repositories
        /// </summary>
        public Dbcontext DbContext { get; set; }

        /// <summary>
        /// handles a command
        /// </summary>
        /// <param name="command"></param>
        /// <returns>a generic response result</returns>
        public abstract Task<ResponseResult<TResponse>> ResolveAsync(TCommand command);
    }
    /// <summary>
    /// abstract implementation of a commandhandler
    /// </summary>
    /// <typeparam name="TCommand">command type</typeparam>
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        /// <summary>
        /// contains all repositories
        /// </summary>
        public Dbcontext DbContext { get; set; }

        /// <summary>
        /// handles a command
        /// </summary>
        /// <param name="command">command type</param>
        /// <returns>a response result</returns>
        public abstract Task<ResponseResult> ResolveAsync(TCommand command);
    }
}
