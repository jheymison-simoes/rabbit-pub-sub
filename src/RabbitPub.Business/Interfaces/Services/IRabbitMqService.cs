using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitPub.Business.Models.RabbiMq;

namespace RabbitPub.Business.Interfaces.Services;

public interface IRabbitMqService
{ 
    IConnection GetConnection();
    IModel GetChannel();
    void CreateConnectionFactory(Uri uri);
    void ExchangeDeclare(RabbitMqExchangeConfig rabbitMqConfig);
    void Publish<T>(RabbitMqPublishConfig<T> rabbitMqConfig);
    void QueueDeclare(RabbiMqQueueConfig rabbitMqConfig);
    EventingBasicConsumer CreateConsumer();
    void BasicConsume(string queueName, bool autoAck, EventingBasicConsumer consumer);
    T DencodingMessage<T>(BasicDeliverEventArgs eventArgs);
    void QueueBind(RabbitMqQueueBindConfig rabbitMqQueueBindConfig);
}