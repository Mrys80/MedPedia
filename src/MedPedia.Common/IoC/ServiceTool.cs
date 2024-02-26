using Microsoft.Extensions.DependencyInjection;

namespace MedPedia.Common.IoC;

public static class ServiceTool
{
    private static IServiceProvider _serviceProvider {  get; set; }

    public static IServiceCollection Create(IServiceCollection services)
    {
        _serviceProvider = services.BuildServiceProvider();
        return services;
    }

    public static T Resolve<T>()
    {
        return _serviceProvider.GetService<T>();
    }
}