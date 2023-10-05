using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.CreateAuthor
{
    public class CreateAuthorCommand : Command<Guid>
    {
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidString(FirstName, "invalid first name")
                .IsValidString(LastName, "invalid last name")
                .Result;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
