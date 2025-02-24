using LearnNetCore.Basic.Dtos;
using System.Net;

namespace LearnNetCore.Basic.Middlewares
{
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMyDependencySingleton _myDependency;
        /**
         *  在 ASP.NET Core 中，通过 UseMiddleware 注册的中间件生命周期默认是 单例（Singleton）
         *  中间件实例在应用启动时被创建且全局唯一,所有后续请求共享同一个实例
         *      1.构造函数仅在应用启动时执行一次
         *      2.若中间件依赖非单例服务(如Scoped/Transient),在构造函数中直接注入会破坏生命周期规则
         *  
         *  范围内服务和暂时性服务必须在 InvokeAsync 方法中进行解析
         *  Scoped 服务：直接注入会触发异常（如 Cannot resolve scoped service from root provider），因为单例中间件持有请求级别的实例12。
         *  Transient 服务：不会立即报错，但单例中间件会长期持有该实例，可能导致资源泄漏。
         */
        public RequestSetOptionsMiddleware(RequestDelegate next, IMyDependencySingleton myDependency)
        {
            Console.WriteLine("RequestSetOptionsMiddleware执行了");
            _next = next;
            _myDependency = myDependency;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var option = httpContext.Request.Query["option"];
            if(!string.IsNullOrWhiteSpace(option))
            {
                httpContext.Response.Headers["options"] = WebUtility.HtmlEncode(option + _myDependency.Id.ToString("N").Substring(0, 5));
            }
            await _next(httpContext);
        }
    }
}
