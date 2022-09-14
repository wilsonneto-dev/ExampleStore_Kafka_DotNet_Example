namespace ExampleStore.Orders.Domain.Order;

public enum OrderStatus
{
    Processing = 0,
    Approved = 1,
    PaymentError = 2,
    Error = 3
}