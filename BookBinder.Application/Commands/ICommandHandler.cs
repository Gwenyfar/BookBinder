using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands
{
    public interface ICommandHandler<TCommand, TResponse> where TCommand : Command<TResponse>
    {
       Task<ResponseResult<TResponse>> ResolveAsync(TCommand command);
       Dbcontext DbContext { get; set; }
    }
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task<ResponseResult> ResolveAsync(TCommand command);
    }
    public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>  where TCommand : Command<TResponse>
    {
        public Dbcontext DbContext { get; set; }

        public abstract Task<ResponseResult<TResponse>> ResolveAsync(TCommand command);
    }
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : Command
    {
        public Dbcontext DbContext { get; set; }

        public abstract Task<ResponseResult> ResolveAsync(TCommand command);
    }
}
