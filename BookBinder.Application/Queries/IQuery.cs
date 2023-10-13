using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Queries
{
    /// <summary>
    /// underling interface for all queries
    /// </summary>
    /// <typeparam name="TResponse">response type</typeparam>
    public interface IQuery<TResponse> : IRequestValidator
    {
    }
}
