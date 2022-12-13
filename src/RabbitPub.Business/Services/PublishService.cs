using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitPub.Business.Interfaces.Services;
using RabbitPub.Business.Models;
using RabbitPub.Business.Models.RabbiMq;

namespace RabbitPub.Business.Services;

public class PublishService : BaseService, IPublishService
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly AppSettings _appSettings;
    
    public PublishService(
        IMapper mapper,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IRabbitMqService rabbitMqService,
        IOptions<AppSettings> appSettings) : base(mapper, resourceManager, cultureInfo)
    {
        _rabbitMqService = rabbitMqService;
        _appSettings = appSettings.Value;
    }

    public async Task<string> PublishMessage(string message)
    {
        _rabbitMqService.ExchangeDeclare(new RabbitMqExchangeConfig()
        {
            ExchangeName = _appSettings.RabbitExchangeName,
            ExchangeType = ExchangeType.Fanout,
            Durable = true
        });
                
        _rabbitMqService.Publish(new RabbitMqPublishConfig<PublishMessage>()
        {
            ExchangeName = _appSettings.RabbitExchangeName,
            RoutinKey = _appSettings.RabbitRoutingKey,
            Message = new PublishMessage()
            {
                Date = DateTime.UtcNow,
                Message = message
            }
        });

        return await Task.Run(() => "Message send success!");
    }
}