using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Security
{
    public interface IPasswordManager
    {
        string HashPassword(string password);
        bool VerifyPassword(string passwordString, string passwordHash);
    }
}
