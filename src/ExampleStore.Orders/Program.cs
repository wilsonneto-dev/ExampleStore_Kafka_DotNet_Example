using ExampleStore.Orders.Application.Integration.Interfaces;
using ExampleStore.Orders.Application.UseCases;
using ExampleStore.Orders.Application.UseCases.Interfaces;
using ExampleStore.Orders.Domain.Order;
using ExampleStore.Orders.Infra.KafkaEventBus;
using ExampleStore.Orders.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUseCase<CreateOrderInput, CreateOrderOutput>, CreateOrder>();
builder.Services.AddScoped<IEventBus, KafkaEventBus>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/orders", async (CreateOrderInput createOrderInput, IUseCase<CreateOrderInput, CreateOrderOutput> useCase) 
    => await useCase.Handle(createOrderInput));

app.Run();
