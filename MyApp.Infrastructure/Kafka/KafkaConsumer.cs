using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using MyApp.Core.Kafka.Events;

namespace MyApp.Infrastructure.Kafka;

public class KafkaConsumer : BackgroundService
{
    private readonly KafkaOptions _options;

    public KafkaConsumer(KafkaOptions options)
    {
        _options = options;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            SaslMechanism = SaslMechanism.Plain,
            SaslUsername = _options.ApiKey,
            SaslPassword = _options.ApiSecret,

            GroupId = _options.GroupId,

            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using var consumer =
            new ConsumerBuilder<string, string>(config).Build();

        consumer.Subscribe(_options.Topic);

        Console.WriteLine(
            $"Kafka consumer listening to '{_options.Topic}'...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);

                var employee =
                    JsonSerializer.Deserialize<EmployeeAddedEvent>(
                        result.Message.Value);

                Console.WriteLine("================================");
                Console.WriteLine("EMPLOYEE ADDED EVENT RECEIVED");
                Console.WriteLine($"Employee Id: {employee?.EmployeeId}");
                Console.WriteLine($"Name: {employee?.Name}");
                Console.WriteLine($"Email: {employee?.Email}");
                Console.WriteLine("================================");
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Kafka consumer error: {ex.Message}");
            }
        }

        consumer.Close();
    }
}