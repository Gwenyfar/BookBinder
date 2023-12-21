using Bookbinder.Domain.DTOs;
using BookBinder.Application;
using BookBinder.Application.Commands.LogIn;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookBinder.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IApplication application) : base(application)
        {
            
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var command = new LoginCommand
            {
                Email = login.Email,
                Password = login.Password
            };
            var response = await Application.ExecuteCommandAsync<LoginCommand,ClaimsPrincipal>(command);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, response.Response, new AuthenticationProperties { IsPersistent = true });
            return Ok();
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
