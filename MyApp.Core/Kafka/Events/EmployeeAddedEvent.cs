namespace MyApp.Core.Kafka.Events;

public class EmployeeAddedEvent
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}