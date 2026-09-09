using MediatR;
using MyApp.Core.Entities;

namespace MyApp.Application.Queries
{
    // public class AddEmployeeCommand
    // {
        
    // }

    // as AddEmployeeCommand is simple an will have only some proprties . s its better practice to use record
    public record GetEmployeeByIdQuery( Guid id): IRequest<EmployeeEntity>;
    public class GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeByIdQuery, EmployeeEntity>
    {
        public async Task<EmployeeEntity> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
           return await employeeRepository.GetEmployeesByIdAsync(request.id);
        }
    }

}