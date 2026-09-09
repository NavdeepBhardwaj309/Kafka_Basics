
using MediatR;
using MyApp.Core.Entities;
using MyApp.Core.Kafka.Events;
using MyApp.Core.Interfaces;
namespace MyApp.Application.Commands
{
    // public class AddEmployeeCommand
    // {
        
    // }

    // as AddEmployeeCommand is simple an will have only some proprties . s its better practice to use record
    public record AddEmployeeCommand(EmployeeEntity Employee): IRequest<EmployeeEntity>;
    public class AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEventPublisher _eventPublisher) : IRequestHandler<AddEmployeeCommand, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
           var employee= await employeeRepository.AddEmpolyeeAsync(request.Employee);
          

        await _eventPublisher.PublishEmployeeAddedAsync(
            new EmployeeAddedEvent
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                Email = employee.Email
            });

        return employee;
        }
    }

}