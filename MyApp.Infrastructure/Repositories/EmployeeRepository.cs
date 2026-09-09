
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositoires
{
    public class EmployeeRepository :IEmployeeRepository
    { private readonly AppDbContext dbContext;
        public EmployeeRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployeesByIdAsync(Guid id)
        {
            return await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EmployeeEntity> AddEmpolyeeAsync(EmployeeEntity entity)
        {
            entity.Id = Guid.NewGuid();
            dbContext.Employees.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<EmployeeEntity> UpdateEmpolyeeAsync(Guid employeeId, EmployeeEntity entity)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee is not null)
            {
                employee.Name = entity.Name;
                employee.Email = entity.Email;
                employee.Phone = entity.Phone;
                await dbContext.SaveChangesAsync();
                return employee;
            }

            return entity;
        }
         public async Task<bool> DeleteEmpolyeeAsync(Guid employeeId)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee is not null)
            {
                dbContext.Employees.Remove(employee);
                return await dbContext.SaveChangesAsync()>0;
                
            }

            return false;
        }
    }
}