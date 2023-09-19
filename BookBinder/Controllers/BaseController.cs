using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    
    public class BaseController : ControllerBase
    {
        public BaseController(IContainer container)
        {
            Container = container;
        }
        public IContainer Container { get; set; }
    }
}
