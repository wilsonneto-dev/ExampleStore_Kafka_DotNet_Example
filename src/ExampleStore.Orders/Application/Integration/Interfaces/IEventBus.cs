namespace ExampleStore.Orders.Application.Integration.Interfaces;

public interface IEventBus
{
    Task Publish(IIntegrationEvent integrationEvent);
}
