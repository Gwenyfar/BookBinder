using BookBinder.Application;
using BookBinder.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SingleSignOn()
        {
            var token = await HttpContext.GetTokenAsync("Bearer");
            _logger.LogInformation(token);
            var response = new ResponseResult { Successful = true };
            return FetchResponse(response);
        }
    }
}
