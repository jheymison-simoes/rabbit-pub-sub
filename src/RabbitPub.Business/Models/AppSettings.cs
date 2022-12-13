namespace RabbitPub.Business.Models;

public class AppSettings
{
    public string RabbitMqConnection { get; set; }
    public string RabbitExchangeName { get; set; }
    public string RabbitQueueName { get; set; }
    public string RabbitRoutingKey { get; set; }
}