using RabbitPub.Business.Interfaces.Services;
using RabbitPub.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitPub.Business.Configuration
{
    public static class BussinessDependencyInjectionConfig
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.DependencyInjectionValidators();
            services.DependencyInjectionServices();
        }

        private static void DependencyInjectionValidators(this IServiceCollection services)
        {
            
        }

        private static void DependencyInjectionServices(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddScoped<IPublishService, PublishService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
        }
    }
}