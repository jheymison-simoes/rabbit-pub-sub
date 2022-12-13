using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitPub.Business.Interfaces.Services;
using RabbitPub.Business.Models;
using RabbitPub.Business.Models.RabbiMq;

namespace RabbitPub.Business.Services;

public class RabbitMqService : IRabbitMqService
{
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqService(IOptions<AppSettings> appSettings)
    {
        CreateConnectionFactory(new Uri(appSettings.Value.RabbitMqConnection));
    }
    
    public IConnection GetConnection() => _connection;
    public IModel GetChannel() => _channel;

    public void CreateConnectionFactory(Uri uri)
    {
        try
        {
            var factory = new ConnectionFactory() { Uri = uri };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public void ExchangeDeclare(RabbitMqExchangeConfig rabbitMqConfig)
    {
        try
        {
            _channel.ExchangeDeclare(
                rabbitMqConfig.ExchangeName,
                rabbitMqConfig.ExchangeType,
                rabbitMqConfig.Durable,
                rabbitMqConfig.AutoDelete,
                rabbitMqConfig.Arguments
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public void QueueDeclare(RabbiMqQueueConfig rabbitMqQueueConfig)
    {
        try
        {
            _channel.QueueDeclare(
                rabbitMqQueueConfig.QueueName,
                rabbitMqQueueConfig.Durable,
                rabbitMqQueueConfig.Exclusive,
                rabbitMqQueueConfig.AutoDelete,
                rabbitMqQueueConfig.Arguments
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public void QueueBind(RabbitMqQueueBindConfig rabbitMqQueueBindConfig)
    {
        try
        {
            _channel.QueueBind(
                rabbitMqQueueBindConfig.QueueName,
                rabbitMqQueueBindConfig.ExchangeName,
                rabbitMqQueueBindConfig.RoutingKey
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public EventingBasicConsumer CreateConsumer()
    {
        try
        {
            return new EventingBasicConsumer(_channel);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public void BasicConsume(string queueName, bool autoAck, EventingBasicConsumer consumer)
    {
        try
        {
            _channel.BasicConsume(queueName, autoAck, consumer);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public void Publish<T>(RabbitMqPublishConfig<T> rabbitMqConfig)
    {
        try
        {
            var body = EncodingMessage(rabbitMqConfig.Message);

            _channel.BasicPublish(
                rabbitMqConfig.ExchangeName,
                rabbitMqConfig.RoutinKey,
                rabbitMqConfig.Mandatory,
                rabbitMqConfig.BasicProperties,
                body
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    
    public T DencodingMessage<T>(BasicDeliverEventArgs eventArgs)
    {
        var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
        var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var result = JsonSerializer.Deserialize<T>(message, options);
        return result;
    }
    
    #region Private Methods
    private static byte[] EncodingMessage<T>(T message)
    {
        var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonSerialize = JsonSerializer.Serialize(message, options);
        return Encoding.UTF8.GetBytes(jsonSerialize);
    }
    #endregion
}