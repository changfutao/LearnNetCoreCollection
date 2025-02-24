using Microsoft.Extensions.Primitives;

namespace LearnNetCore.Basic.Middlewares
{
    public class RequestIpMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestIpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var forwardedForHeader = httpContext.Request.Headers["X-Forwarded-For"];
            if (!StringValues.IsNullOrEmpty(forwardedForHeader))
            {
                var ips = forwardedForHeader.ToString().Split(',');
                var clientIp = ips[0].Trim(); // 第一个 IP 是原始客户端地址 
                Console.WriteLine(clientIp);
            }
            else
            {
                string? clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
                Console.WriteLine(clientIp);
            }
            await _next(httpContext);
        }
    }
}
