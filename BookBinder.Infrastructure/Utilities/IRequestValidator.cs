using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Utilities
{
    public interface IRequestValidator
    {
        ResponseResult ValidateBody();
    }
}
