using RabbitPub.Business.Models;

namespace RabbitPub.Business.Interfaces.Services;

public interface ISubscriptionService
{
    Task ReceiveMessage(PublishMessage message);
}