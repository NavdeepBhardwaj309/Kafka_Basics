namespace MyApp.Infrastructure.Kafka;

public class KafkaOptions
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string Topic { get; set; } = "employee-added";
    public string GroupId { get; set; } = "employee-consumer";
}