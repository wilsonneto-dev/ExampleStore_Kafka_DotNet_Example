using ExampleStore.Orders.Application.Integration.Interfaces;

namespace ExampleStore.Orders.Application.Integration.Events;

public record OrderCreatedEvent(Guid OrderId, int ProductId, int Quantity, DateTime Date) : IIntegrationEvent;