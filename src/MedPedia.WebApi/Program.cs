using Autofac.Extensions.DependencyInjection;

namespace Teb.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            //.ConfigureContainer<ContainerBuilder>(builder =>
            //{
            //    builder.RegisterModule(new AutofacBusinessModule());
            //})
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}