namespace RabbitPub.Business.Models.RabbiMq;

public class RabbitMqExchangeConfig
{
    public string ExchangeName { get; set; }
    public string ExchangeType { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; }
    public IDictionary<string, object> Arguments { get; set; }   
}