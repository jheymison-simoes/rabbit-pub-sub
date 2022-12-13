namespace RabbitPub.Business.Models.RabbiMq;

public class RabbiMqQueueConfig
{
    public string QueueName { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; }
    public bool Exclusive { get; set; }
    public IDictionary<string, object> Arguments { get; set; }
}