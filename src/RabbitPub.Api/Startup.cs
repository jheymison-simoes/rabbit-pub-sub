using RabbitPub.Api.Configuration;

namespace RabbitPub.Api;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration(Configuration);
        services.AddResourceConfiguration();
        services.DependencyInjection(Configuration);
        services.AddSwaggerConfiguration();
        services.AddBackgroundServicesConfig();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseApiConfiguration(env);
        app.UseSwaggerConfiguration();
    }
}