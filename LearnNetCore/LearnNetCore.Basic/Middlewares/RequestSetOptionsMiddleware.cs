using System.Net;

namespace LearnNetCore.Basic.Middlewares
{
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestSetOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var option = httpContext.Request.Query["option"];
            if(!string.IsNullOrWhiteSpace(option))
            {
                httpContext.Response.Headers["options"] = WebUtility.HtmlEncode(option + new Random().Next(10));
            }
            await _next(httpContext);
        }
    }
}
