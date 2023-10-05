using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands
{
    
    public abstract class Command : IRequestValidator
    {
        public abstract ResponseResult ValidateBody();
    }
    public abstract class Command<TResponse> : Command
    {
        
    }
}
