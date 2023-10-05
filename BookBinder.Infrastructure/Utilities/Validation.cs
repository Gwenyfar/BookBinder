using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookBinder.Infrastructure.Utilities
{
    public class Validation
    {
        public Validation()
        {
            Result = ResponseResult.Success();
        }

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
        public Validation IsValidGuid(Guid input, string errorMessage)
        {
            var isInvalid = input.ToString().Length != 32;
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
        public ResponseResult Result { get; set; }
    }
}
