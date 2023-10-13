using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.UpdateAuthor
{
    /// <summary>
    /// request to update an author
    /// </summary>
    public class UpdateAuthorCommand : Command
    {
        /// <summary>
        /// validation for a request body
        /// </summary>
        /// <returns>a response result depending on if validation passed or not</returns>
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidGuid(Id, "invalid id")
                .IsValidString(FirstName, "invalid first name")
                .IsValidString(LastName, "invalid last name")
                .Result;
        }

        /// <summary>
        /// existing author id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// updated first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// updated last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// updated email
        /// </summary>
        public string Email { get; set; }
    }
}
