using Confluent.Kafka;
using ExampleStore.Orders.Application.Integration.Interfaces;
using Newtonsoft.Json;

namespace ExampleStore.Orders.Infra.KafkaEventBus;

internal class KafkaEventBus : IEventBus
{
    public string TopicName { get; set; } = "orders";
    public string KafkaServer { get; set; } = "localhost:9092";

    public async Task Publish(IIntegrationEvent integrationEvent)
    {
        try
        {
            var config = new ProducerConfig
            {
                BootstrapServers = KafkaServer
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(
                    TopicName,
                    new Message<Null, string>()
                        { Value = JsonConvert.SerializeObject(integrationEvent) }
                );

                Console.WriteLine(
                    $"Mensagem: {JsonConvert.SerializeObject(integrationEvent)} | " +
                    $"Status: {result.Status.ToString()}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Exceção: {ex.GetType().FullName} | " +
                $"Mensagem: {ex.Message}");
        }
    }
}