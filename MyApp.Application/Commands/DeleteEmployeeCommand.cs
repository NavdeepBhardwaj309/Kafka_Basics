

using MediatR;
using MyApp.Core.Entities;

namespace MyApp.Application.Commands
{
    // public class AddEmployeeCommand
    // {
        
    // }

    // as AddEmployeeCommand is simple an will have only some proprties . s its better practice to use record
    public record DeleteEmployeeCommand(Guid employeeId): IRequest<bool>;
    public class  DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<DeleteEmployeeCommand,bool>
    {
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
           return await employeeRepository.DeleteEmpolyeeAsync(request.employeeId);
        }
    }

}