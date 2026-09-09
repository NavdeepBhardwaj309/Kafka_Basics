using Confluent.Kafka;
using System.Text.Json;
using MyApp.Core.Kafka.Events;
using MyApp.Core.Interfaces;
namespace MyApp.Infrastructure.Kafka;

public class KafkaProducer :IEventPublisher
{
    private readonly KafkaOptions _options;

    public KafkaProducer(KafkaOptions options)
    {
        _options = options;
    }

    public async Task PublishEmployeeAddedAsync(
        EmployeeAddedEvent employeeEvent)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = _options.ApiKey,
            SaslPassword = _options.ApiSecret
        };

        using var producer =
            new ProducerBuilder<string, string>(config).Build();

        var message = new Message<string, string>
        {
            Key = employeeEvent.EmployeeId.ToString(),
            Value = JsonSerializer.Serialize(employeeEvent)
        };

        var result = await producer.ProduceAsync(
            _options.Topic,
            message);

        Console.WriteLine(
            $"Kafka message published. Topic: {result.Topic}, Partition: {result.Partition}, Offset: {result.Offset}");
    }
}