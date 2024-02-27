using Autofac;
using MedPedia.Common.Utilities.Settings;
using MedPedia.WebFramework.Configuration;
using MedPedia.WebFramework.Swagger;

namespace MedPedia.WebApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    private readonly Setting _setting;
    private readonly IConfigurationSection _settingSection;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        _settingSection = configuration.GetSection(nameof(Setting));
        _setting = Configuration.GetSection(nameof(Setting)).Get<Setting>();
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext(Configuration);
        services.Configure<Setting>(_settingSection);
        services.AddJwtAuthentication(_setting.JwtSetting);
        services.AddControllers();
        services.AddCustomApiVersioning();
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddCustomCors();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        //Register Services to Autofac ContainerBuilder
        builder.AddServices(Configuration, _setting);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("CORS");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwaggerAndUI();

        app.UseCustomExceptionHandler();

        app.UseStringSanitizer();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        if (env.IsProduction())
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
        else
        {
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}