using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : Command
    {
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidGuid(Id, "invalid id")
                .IsValidString(FirstName, "invalid first name")
                .IsValidString(LastName, "invalid last name")
                .Result;
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
