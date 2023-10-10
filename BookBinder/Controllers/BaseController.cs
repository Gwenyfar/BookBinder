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

        private IActionResult FetchResponse(ResponseResult result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestObjectResult(result)
                HttpStatusCode.
            }
        }
    }
}
