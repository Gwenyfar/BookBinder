using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Security
{
    public interface IJWTProvider
    {
        string GetAccessToken(string email, Guid userId);
    }
}
