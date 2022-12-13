using RabbitPub.Api.BackgroundServices;

namespace RabbitPub.Api.Configuration;

public static class BackgroundServicesConfig
{
    public static void AddBackgroundServicesConfig(this IServiceCollection services)
    {
        services.AddHostedService<RabbitSub>();
    }
}