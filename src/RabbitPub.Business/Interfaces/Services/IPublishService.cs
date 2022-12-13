namespace RabbitPub.Business.Interfaces.Services;

public interface IPublishService
{
    Task<string> PublishMessage(string message);
}