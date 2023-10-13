using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands
{
    /// <summary>
    /// base of all parameterless commands
    /// </summary>
    public abstract class Command : IRequestValidator
    {
        /// <summary>
        /// validates a command
        /// </summary>
        /// <returns>a response result</returns>
        public abstract ResponseResult ValidateBody();
    }

    /// <summary>
    /// base of all commands that return data
    /// </summary>
    /// <typeparam name="TResponse">type of data to return</typeparam>
    public abstract class Command<TResponse> : Command
    {
        
    }
}
