using BookBinder.Application;
using BookBinder.Application.Commands.Login;
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
            var token = await HttpContext.GetTokenAsync("SSO","access_token");
            _logger.LogInformation(token);
            //await HttpContext.SignInAsync("SSO", User, new AuthenticationProperties { IsPersistent = true });
            var response = new ResponseResult { Successful = true };
            return FetchResponse(response);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand logIn)
        {
            var response = await Application.ExecuteCommandAsync<LoginCommand, string>(logIn);
            var token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            _logger.LogInformation(token);
            
            return FetchResponse(response);
        }
    }
}
