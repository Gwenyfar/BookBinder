using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Utilities
{
    /// <summary>
    /// request validator interface
    /// </summary>
    public interface IRequestValidator
    {
        ResponseResult ValidateBody();
    }
}
