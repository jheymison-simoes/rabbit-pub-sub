using System.Globalization;
using System.Resources;
using RabbitPub.Api.Resource;
using RabbitPub.Business.Configuration;
using RabbitPub.Data.Configuration;

namespace RabbitPub.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection DependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        BussinessDependencyInjectionConfig.DependencyInjection(services);
        DataInjectionConfiguration.DependencyInjection(services);

        return services;
    }
}