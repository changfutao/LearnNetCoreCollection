
namespace LearnNetCore.Basic.Middlewares
{
    public class RequestSetOptionsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                // 通过 UseMiddleware 注册的中间件生命周期默认是 单例（Singleton）
                builder.UseMiddleware<RequestSetOptionsMiddleware>();
                builder.UseMiddleware<RequestIpMiddleware>();
                next(builder);
            };
        }
    }
}
