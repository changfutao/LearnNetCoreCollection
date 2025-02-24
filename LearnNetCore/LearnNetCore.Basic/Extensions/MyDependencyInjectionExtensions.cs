using LearnNetCore.Basic.Dtos;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyDependencyInjectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            // 注册同一服务类型的单个服务实例
            services.AddTransient<IMyDependencyTransient, MyDependencyTransient>();
            services.AddScoped<IMyDependencyScoped, MyDependencyScoped>();
            services.AddSingleton<IMyDependencySingleton, MyDependencySingleton>();

            // 注册同一服务类型的多个服务实例
            services.AddTransient<IDependency, DependencyFirst>();
            services.AddTransient<IDependency, DependencySecond>();
            return services;
        }
    }
}
