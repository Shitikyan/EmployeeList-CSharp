using EmployeeManager.Application.Models;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(SearchCriteria searchCriteria);

        Task<Employee?> GetEmployeeByIdAsync(string id);

        Task CreateEmployeeAsync(Employee employee);

        Task UpdateEmployeeAsync(Employee employee);

        Task DeleteEmployeeAsync(string employeeId);
    }
}
