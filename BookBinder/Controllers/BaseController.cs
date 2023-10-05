using Autofac;
using BookBinder.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    
    public class BaseController : ControllerBase
    {
        public BaseController(IApplication application)
        {
            Application = application;
        }
        public IApplication Application { get; set; }
    }
}
