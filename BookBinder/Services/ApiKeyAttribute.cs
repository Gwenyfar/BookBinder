using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookBinder.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiKeyAttribute : Attribute, IAuthorizationFilter
    {
        private const string APIKEYNAME = "XApiKey";
        private readonly IConfiguration _config;

        public ApiKeyAttribute( IConfiguration config)
        {
            _config = config;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var apiKeyIsInHeader = httpContext.Request.Headers.TryGetValue(APIKEYNAME, out var apiKeyFrmHeader);
            var apiKey = _config[APIKEYNAME];
            if ((apiKeyIsInHeader && apiKeyFrmHeader == apiKey) || httpContext.Request.Path.StartsWithSegments("/swagger"))
            {
                return;
            }
            context.Result = new UnauthorizedResult();
        }
    }
}
