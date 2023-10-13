using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Utilities
{
    /// <summary>
    /// models the response to all requests
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// request is successful
        /// </summary>
        public bool Successful { get; set; }
        /// <summary>
        /// request is not successful
        /// </summary>
        public bool NotSuccessful => Successful != true;
        /// <summary>
        /// status code of response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// errors on failure
        /// </summary>
        public List<string> Errors { get; protected set; } = new List<string>();
        /// <summary>
        /// adds error messages to the error list 
        /// </summary>
        /// <param name="errorMessages">error message to add</param>
        /// <returns>returns this response result object</returns>
        public ResponseResult AddErrors(List<string> errorMessages)
        {
            Errors.AddRange(errorMessages);
            return this;
        }

        /// <summary>
        /// adds a single error message to the error list
        /// </summary>
        /// <param name="errorMessage">the error message</param>
        /// <returns>this response result object</returns>
        public ResponseResult AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            return this;
        }

        /// <summary>
        /// returns a successful response result
        /// </summary>
        /// <returns>a response result</returns>
        public static ResponseResult Success()
        {
            return new ResponseResult { Successful = true, StatusCode = HttpStatusCode.OK };
        }

        /// <summary>
        /// returns a failed response result
        /// </summary>
        /// <returns>a response result</returns>
        public static ResponseResult Failed(HttpStatusCode statusCode)
        {
            return new ResponseResult { Successful = false, StatusCode = statusCode };
        }
    }

    /// <summary>
    /// a request response that returns data
    /// </summary>
    /// <typeparam name="T">data type</typeparam>
    public class ResponseResult<T> : ResponseResult
    {
        /// <summary>
        /// returns a successful response
        /// </summary>
        /// <param name="response">data to return</param>
        /// <returns></returns>
        public static ResponseResult<T> Success(T response)
        {
            return new ResponseResult<T> { Response = response, Successful = true, StatusCode = HttpStatusCode.OK };
        }

        /// <summary>
        /// returns a failed response result
        /// </summary>
        /// <param name="statusCode">status code of response</param>
        /// <returns>a response result</returns>
        public static new ResponseResult<T> Failed(HttpStatusCode statusCode)
        {
            return new ResponseResult<T> { Successful = true, StatusCode = statusCode };
        }
        /// <summary>
        /// adds errors to the error list
        /// </summary>
        /// <param name="errorMessages">error messages to add</param>
        /// <returns>a response result</returns>
        public new ResponseResult<T> AddErrors(List<string> errorMessages)
        {
            Errors.AddRange(errorMessages);
            return this;
        }

        /// <summary>
        /// adds an error to error list
        /// </summary>
        /// <param name="errorMessage">the error message</param>
        /// <returns>a response result</returns>
        public new ResponseResult<T> AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            return this;
        }
        /// <summary>
        /// the data to return
        /// </summary>
        public T Response { get; set; }
    }
}
