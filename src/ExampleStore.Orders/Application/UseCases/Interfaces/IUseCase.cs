namespace ExampleStore.Orders.Application.UseCases.Interfaces;

public interface IUseCase<TInput, TOutput>
{
    Task<TOutput> Handle(TInput input);
}

public interface IUseCase<TOutput>
{
    Task<TOutput> Handle();
}
