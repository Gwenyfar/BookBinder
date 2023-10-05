using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries
{
    public interface IQuery<TResponse> : IRequestValidator
    {
    }
}
