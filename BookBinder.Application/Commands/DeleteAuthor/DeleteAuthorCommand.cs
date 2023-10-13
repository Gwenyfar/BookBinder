using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.DeleteAuthor
{
    /// <summary>
    /// a request to delete an author
    /// </summary>
    public class DeleteAuthorCommand : Command
    {
        /// <summary>
        /// validation of request body
        /// </summary>
        /// <returns>a response depending on if validation is passed or not</returns>
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidGuid(Id, "invalid id")
                .Result;
        }
        /// <summary>
        /// id of an author
        /// </summary>
        public Guid Id { get; set; }
    }
}
