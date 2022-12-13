using System.Globalization;
using System.Resources;
using AutoMapper;
using RabbitPub.Business.Interfaces.Services;
using RabbitPub.Business.Models;

namespace RabbitPub.Business.Services;

public class SubscriptionService : BaseService, ISubscriptionService
{
    public SubscriptionService(
        IMapper mapper,
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(mapper, resourceManager, cultureInfo)
    {
    }

    public async Task ReceiveMessage(PublishMessage message)
    {
        Console.WriteLine("Data {0} | Mensagem {1}", message.Date, message.Message);
        await Task.CompletedTask;
    }
}