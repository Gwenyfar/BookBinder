using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.Login
{
    public class LoginCommand : Command<string>
    {
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidEmail(Email, "invalid email")
                .IsValidString(Password, "invalid password")
                .Result;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
