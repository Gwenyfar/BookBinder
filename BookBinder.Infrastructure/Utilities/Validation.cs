using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookBinder.Infrastructure.Utilities
{
    /// <summary>
    /// performs validation on request body
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// constructor
        /// </summary>
        public Validation()
        {
            Result = ResponseResult.Success();
        }

        /// <summary>
        /// validates a string
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>this validation object</returns>
        public Validation IsValidString(string input, string errorMessage)
        {
            var isInvalid = string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input);
            if (isInvalid && Result.Successful)
            {
                Result = ResponseResult.Failed(System.Net.HttpStatusCode.BadRequest).AddError(errorMessage);
            }
            else if(isInvalid && Result.Successful)
            {
                Result.AddError(errorMessage);
            }
            return this;
        }

        /// <summary>
        /// validates a guid
        /// </summary>
        /// <param name="input">input guid</param>
        /// <param name="errorMessage">error message on failure</param>
        /// <returns>this validation object</returns>
        public Validation IsValidGuid(Guid input, string errorMessage)
        {
            var isInvalid = input.Equals(Guid.Empty);
            if (isInvalid && Result.Successful)
            {
                Result = ResponseResult.Failed(System.Net.HttpStatusCode.BadRequest).AddError(errorMessage);
            }
            else if (isInvalid && Result.Successful)
            {
                Result.AddError(errorMessage);
            }
            return this;
        }


        public Validation IsValidEmail(string input, string errorMessage)
        {
            var isValid = new EmailAddressAttribute().IsValid(input);
            if (!isValid && Result.Successful)
            {
                Result = ResponseResult.Failed(System.Net.HttpStatusCode.BadRequest).AddError(errorMessage);
            }
            else if (!isValid && Result.Successful)
            {
                Result.AddError(errorMessage);
            }
            return this;
        }

        /// <summary>
        /// a response after all validation
        /// </summary>
        public ResponseResult Result { get; set; }
    }
}
