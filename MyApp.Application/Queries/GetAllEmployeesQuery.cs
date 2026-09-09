
using MediatR;
using MyApp.Core.Entities;

namespace MyApp.Application.Queries
{
    // public class AddEmployeeCommand
    // {
        
    // }

    // as AddEmployeeCommand is simple an will have only some proprties . s its better practice to use record
    public record GetAllEmployeesQuery( ): IRequest<IEnumerable<EmployeeEntity>>;
    public class GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeEntity>>
    {
        public async Task<IEnumerable<EmployeeEntity>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
           return await employeeRepository.GetEmployees();
        }
    }

}