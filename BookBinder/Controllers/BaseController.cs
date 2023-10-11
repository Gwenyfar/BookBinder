using Autofac;
using BookBinder.Application;
using BookBinder.Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookBinder.Controllers
{
    
    public class BaseController : ControllerBase
    {
        public BaseController(IApplication application)
        {
            Application = application;
        }
        public IApplication Application { get; set; }

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

    public class InternalServerObjectResult : ObjectResult
    {
        public InternalServerObjectResult(object? value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
