namespace RabbitPub.Business.Models.RabbiMq;

public class RabbitMqQueueBindConfig
{
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string RoutingKey { get; set; }
}