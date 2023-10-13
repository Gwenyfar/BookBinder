using BookBinder.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Commands.CreateAuthor
{
    /// <summary>
    /// represents a request to create an author
    /// </summary>
    public class CreateAuthorCommand : Command<Guid>
    {
        /// <summary>
        /// validation for this request
        /// </summary>
        /// <returns>a response based on passed or failed validations</returns>
        public override ResponseResult ValidateBody()
        {
            return new Validation()
                .IsValidString(FirstName, "invalid first name")
                .IsValidString(LastName, "invalid last name")
                .Result;
        }
        /// <summary>
        /// author's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// author's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// author's email
        /// </summary>
        public string Email { get; set; }
    }
}
