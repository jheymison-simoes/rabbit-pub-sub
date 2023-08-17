using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitPub.Business.Interfaces.Services;
using RabbitPub.Business.Models;
using RabbitPub.Business.Models.RabbiMq;

namespace RabbitPub.Api.BackgroundServices;

public class RabbitSub : BackgroundService
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IServiceProvider _serviceProvider;
    private readonly AppSettings _appSettings;

    public RabbitSub(
        IOptions<AppSettings> appSettings, 
        IServiceProvider serviceProvider,
        IRabbitMqService rabbitMqService)
    {
        _serviceProvider = serviceProvider;
        _appSettings = appSettings.Value;
        _rabbitMqService = rabbitMqService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = GenerateConsumer();
        consumer.Received += ConsumerReceived;
        _rabbitMqService.BasicConsume(_appSettings.RabbitQueueName, true, consumer);
        return Task.CompletedTask;
    }

    private EventingBasicConsumer GenerateConsumer()
    {
        _rabbitMqService.ExchangeDeclare(new RabbitMqExchangeConfig()
        {
            ExchangeName = _appSettings.RabbitExchangeName,
            ExchangeType = ExchangeType.Fanout,
            Durable = true
        });

        _rabbitMqService.QueueDeclare(new RabbiMqQueueConfig()
        {
            QueueName = _appSettings.RabbitQueueName,
            Durable = true
        });
        
        _rabbitMqService.QueueBind(new RabbitMqQueueBindConfig()
        {
            QueueName = _appSettings.RabbitQueueName,
            ExchangeName = _appSettings.RabbitExchangeName,
            RoutingKey = _appSettings.RabbitRoutingKey
        });

        return _rabbitMqService.CreateConsumer();
    }

    private async void ConsumerReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        using var scope = _serviceProvider.CreateScope();

        try
        {
            var message = _rabbitMqService.DencodingMessage<PublishMessage>(eventArgs);
            var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
            await subscriptionService.ReceiveMessage(message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

}
