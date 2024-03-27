using BookBinder.Application;
using BookBinder.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        [HttpPost("sso")]
        [Authorize("SSO")]
        public async Task<IActionResult> SingleSignOn()
        {
            var token = await HttpContext.GetTokenAsync("Bearer");
            _logger.LogInformation(token);
            await HttpContext.SignInAsync("SSO", User, new AuthenticationProperties { IsPersistent = true });
            var response = new ResponseResult { Successful = true };
            return FetchResponse(response);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            var token = await HttpContext.GetTokenAsync("Bearer");
            _logger.LogInformation(token);
            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, User, new AuthenticationProperties { IsPersistent = true });
            var response = new ResponseResult { Successful = true };
            return FetchResponse(response);
        }
    }
}
