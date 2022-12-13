using RabbitMQ.Client;

namespace RabbitPub.Business.Models.RabbiMq;

public class RabbitMqPublishConfig<T>
{
    public string ExchangeName { get; set; }
    public string RoutinKey { get; set; }
    public bool Mandatory { get; set; }
    public IBasicProperties BasicProperties { get; set; }
    public T Message { get; set; }
}