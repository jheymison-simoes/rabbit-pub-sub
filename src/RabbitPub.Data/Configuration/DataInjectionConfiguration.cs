using Microsoft.Extensions.DependencyInjection;

namespace RabbitPub.Data.Configuration;

public static class DataInjectionConfiguration
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        InjectionDependencyRepository(services);
        InjectionDependencyUniOfWork(services);
    }

    private static void InjectionDependencyRepository(IServiceCollection services)
    {
    }

    private static void InjectionDependencyUniOfWork(IServiceCollection services)
    {
    }
}