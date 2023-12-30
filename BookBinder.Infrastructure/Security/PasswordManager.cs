using Bookbinder.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Security
{
    public class PasswordManager : IPasswordManager
    {
        public string HashPassword(string password)
        {
            return new PasswordHasher<ApplicationUser>().HashPassword(null, password);
        }

        public bool VerifyPassword(string passwordString, string passwordHash)
        {
            var result = new PasswordHasher<ApplicationUser>().VerifyHashedPassword(null, passwordHash, passwordString);
            return result == PasswordVerificationResult.Success;
        }
    }
}
