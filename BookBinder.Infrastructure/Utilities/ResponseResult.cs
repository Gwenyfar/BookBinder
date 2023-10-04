using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Utilities
{
    public class ResponseResult
    {
        public bool Successful { get; set; }
        public bool NotSuccessful => Successful != true;
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; protected set; }

        public ResponseResult AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            return this;
        }
        public static ResponseResult Success()
        {
            return new ResponseResult { Successful = true, StatusCode = HttpStatusCode.OK };
        }

        public static ResponseResult Failed(HttpStatusCode statusCode)
        {
            return new ResponseResult { Successful = false, StatusCode = statusCode };
        }
    }

    public class ResponseResult<T> : ResponseResult
    {
        public static ResponseResult<T> Success(T response)
        {
            return new ResponseResult<T> { Response = response, Successful = true, StatusCode = HttpStatusCode.OK };
        }

        public static new ResponseResult<T> Failed(HttpStatusCode statusCode)
        {
            return new ResponseResult<T> { Successful = true, StatusCode = statusCode };
        }

        public new ResponseResult<T> AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            return this;
        }
        public T Response { get; set; }
    }
}
