using LearnNetCore.Basic.Dtos;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MyDependencyInjectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            services.AddTransient<IMyDependencyTransient, MyDependencyTransient>();
            services.AddScoped<IMyDependencyScoped, MyDependencyScoped>();
            services.AddSingleton<IMyDependencySingleton, MyDependencySingleton>();
            return services;
        }
    }
}
