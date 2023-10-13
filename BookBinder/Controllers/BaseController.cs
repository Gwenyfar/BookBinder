using Autofac;
using BookBinder.Application;
using BookBinder.Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookBinder.Controllers
{
    /// <summary>
    /// base class for all controllers
    /// </summary>
    
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// a constructor
        /// </summary>
        /// <param name="application">grants access to applicaion</param>
        public BaseController(IApplication application)
        {
            Application = application;
        }

        /// <summary>
        /// application representation
        /// </summary>
        public IApplication Application { get; set; }

        /// <summary>
        /// returns an object response type based on the status code input
        /// </summary>
        /// <param name="result">a response from the application</param>
        /// <returns>an object result</returns>
        protected IActionResult FetchResponse(ResponseResult result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestObjectResult(result),
                HttpStatusCode.NotFound => new NotFoundObjectResult(result),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(result),
                HttpStatusCode.InternalServerError => new InternalServerObjectResult(result),
                HttpStatusCode.OK => new OkObjectResult(result),
                _ => new ObjectResult(result)
            };
        }
    }

    /// <summary>
    /// an object result response for an internal server error
    /// </summary>
    public class InternalServerObjectResult : ObjectResult
    {
        public InternalServerObjectResult(object? value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
