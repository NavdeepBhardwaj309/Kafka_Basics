

using MediatR;
using MyApp.Core.Entities;

namespace MyApp.Application.Commands
{
    // public class AddEmployeeCommand
    // {
        
    // }

    // as AddEmployeeCommand is simple an will have only some proprties . s its better practice to use record
    public record UpdateEmployeeCommand(Guid employeeId, EmployeeEntity  Employee): IRequest<EmployeeEntity>;
    public class UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<UpdateEmployeeCommand, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
           return await employeeRepository.UpdateEmpolyeeAsync(request.employeeId, request.Employee);
        }
    }

}