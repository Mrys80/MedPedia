using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.Scanning;
using Castle.DynamicProxy;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Interceptors;
using MedPedia.Common.Utilities.Settings;
using MedPedia.WebFramework.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Teb.Data.Abstracts;
using Teb.Data.Concretes.Repositories;

namespace MedPedia.WebFramework.Configuration;

public static class AutofacConfigurationExtensions
{
    public static void AddServices(this ContainerBuilder containerBuilder, IConfiguration configuration,
        Setting setting)
    {
        containerBuilder.Register(c => c.Resolve<IConnectionMultiplexer>().GetDatabase()).As<IDatabase>()
            .SingleInstance();

        containerBuilder.RegisterType<SessionValidatorAttribute>();

        containerBuilder.RegisterType<DapperService>().As<IDapperService>()
            .WithParameter("connectionString", configuration.GetConnectionString("SqlServer"))
            .InstancePerLifetimeScope();

        containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();

        containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        containerBuilder.RegisterGeneric(typeof(ViewRepository<>)).As(typeof(IViewRepository<>))
            .InstancePerLifetimeScope();

        containerBuilder.RegisterAllAssembly()
            .AssignableTo<IScopedDependency>()
            .AsImplementedInterfaces()
            .EnableAspectInterceptor()
            .InstancePerLifetimeScope();

        containerBuilder.RegisterAllAssembly()
            .AssignableTo<ITransientDependency>()
            .AsImplementedInterfaces()
            .EnableAspectInterceptor()
            .InstancePerDependency();

        containerBuilder.RegisterAllAssembly()
            .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
            .EnableAspectInterceptor()
            .SingleInstance();
    }

    private static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterAllAssembly(
        this ContainerBuilder containerBuilder)
    {
        return containerBuilder.RegisterAssemblyTypes(Conf.GetAllAssemblies());
    }

    public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> EnableAspectInterceptor(
        this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder)
    {
        return registrationBuilder.EnableInterfaceInterceptors(new ProxyGenerationOptions()
        {
            Selector = new AspectInterceptorSelector()
        });
    }
}