using LearnNetCore.Basic.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 选项
/// </summary>
public static class OptionsExtension
{
    public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // 注意: 选项Configure不支持string类型
        services.Configure<MyPosition>(configuration.GetSection(MyPosition.Name));
        return services;
    }
}

