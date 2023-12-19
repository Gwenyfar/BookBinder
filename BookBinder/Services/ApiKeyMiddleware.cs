namespace BookBinder.Services
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "XApiKey";
        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            var apiKeyIsInHeader = context.Request.Headers.TryGetValue(APIKEY, out var apiKeyFrmHeader);
            var apiKey = config[APIKEY];
            if ((apiKeyIsInHeader && apiKeyFrmHeader == apiKey) || context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid Api Key");
        }
    }

    public static class ApiKeyMiddlewareExtension
    {
        public static IApplicationBuilder UseApiKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>();
        }
    }
}
