using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.LogIn
{
    public class LoginCommand : Command<ClaimsPrincipal>
    {
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidString(Password, "password is invalid")
                .IsValidEmail(Email, "email is invalid")
                .Result;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
