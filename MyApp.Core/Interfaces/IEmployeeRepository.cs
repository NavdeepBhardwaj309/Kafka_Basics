
using MyApp.Core.Entities;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeEntity>> GetEmployees();
    Task<EmployeeEntity> GetEmployeesByIdAsync(Guid id);
    Task<EmployeeEntity> AddEmpolyeeAsync(EmployeeEntity entity);
    Task<EmployeeEntity> UpdateEmpolyeeAsync(Guid employeeId, EmployeeEntity entity);
    Task<bool> DeleteEmpolyeeAsync(Guid emloyeeId);
}