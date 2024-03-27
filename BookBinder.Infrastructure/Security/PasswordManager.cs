using BookBinder.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Security
{
    public class PasswordManager
    {
        public string HashPassword(string password)
        {
            return new PasswordHasher<User>().HashPassword(null, password);
        }

        public bool VerifyPassword(string passwordHash, string password)
        {
            var result = new PasswordHasher<User>().VerifyHashedPassword(null, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
