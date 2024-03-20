using BookBinder.Application;
using BookBinder.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly ILogger _logger;
        public AuthenticationController(IApplication application, ILogger logger) : base(application)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SingleSignOn(string token)
        {
            _logger.LogInformation(token);
            var response = new ResponseResult { Successful = true };
            return FetchResponse(response);
        }
    }
}
