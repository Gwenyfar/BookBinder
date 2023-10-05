using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : Command
    {
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidGuid(Id, "invalid id")
                .Result;
        }
        public Guid Id { get; set; }
    }
}
