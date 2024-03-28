using FluentMigrator.Infrastructure;
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

        public Validation IsValidEmail(string input, string error)
        {
            var passed  = new EmailAddressAttribute().IsValid(input);
            Validate(passed, error);
            return this;
        }

        /// <summary>
        /// validates a string
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="errorMessage">error message</param>
        /// <returns>this validation object</returns>
        public Validation IsValidString(string input, string errorMessage)
        {
            var passed = !(string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input));
            Validate(passed, errorMessage);
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
            var passed = input.ToString().Length == 32;
            Validate(passed, errorMessage);
            return this;
        }

        public void Validate(bool passed, string error)
        {
            if (!passed && Result.Successful)
            {
                Result = ResponseResult.Failed(System.Net.HttpStatusCode.BadRequest).AddError(error);
            }
            else if (passed && Result.NotSuccessful)
            {
                Result.AddError(error);
            }
        }
        /// <summary>
        /// a response after all validation
        /// </summary>
        public ResponseResult Result { get; set; }
    }
}
