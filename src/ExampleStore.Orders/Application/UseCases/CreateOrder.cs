using ExampleStore.Orders.Application.Integration.Events;
using ExampleStore.Orders.Application.Integration.Interfaces;
using ExampleStore.Orders.Application.UseCases.Interfaces;
using ExampleStore.Orders.Domain.Order;

namespace ExampleStore.Orders.Application.UseCases;

public record CreateOrderInput(int ProductId, int Quantity);
public record CreateOrderOutput(Guid id, string status);

internal class CreateOrder : IUseCase<CreateOrderInput, CreateOrderOutput>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventBus _eventBus;

    public CreateOrder(IOrderRepository orderRepository, IEventBus eventBus)
    {
        _orderRepository = orderRepository;
        _eventBus = eventBus;
    }

    public async Task<CreateOrderOutput> Handle(CreateOrderInput input)
    {
        ValidateInput(input);
        var order = new Order(input.ProductId, input.Quantity);
        await _orderRepository.Save(order);
        await _eventBus.Publish(new OrderCreatedEvent(
            order.Id,
            order.ProductId,
            order.Quantity,
            order.Date));
        return new CreateOrderOutput(order.Id, order.Status.ToString());
    }

    private void ValidateInput(CreateOrderInput input)
    {
        if (input.Quantity <= 0 || input.ProductId <= 0)
            throw new ArgumentException($"{nameof(input.Quantity)} e {nameof(input.ProductId)} devem ser positivos maiores que 0");
    }
}
