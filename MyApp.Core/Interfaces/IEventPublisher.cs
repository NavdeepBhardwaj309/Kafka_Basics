using MyApp.Core.Kafka.Events;

namespace MyApp.Core.Interfaces;

public interface IEventPublisher
{
    Task PublishEmployeeAddedAsync(
        EmployeeAddedEvent employeeEvent);
}