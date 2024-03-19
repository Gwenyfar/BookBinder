using BookBinder.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IApplication application) : base(application)
        {
        }
    }
}
