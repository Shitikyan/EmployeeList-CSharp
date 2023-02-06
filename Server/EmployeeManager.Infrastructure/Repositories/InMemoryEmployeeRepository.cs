using EmployeeManager.Application.Abstractions;
using EmployeeManager.Application.Models;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();

        public Task CreateEmployeeAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid().ToString();

            _employees.Add(employee);

            return Task.CompletedTask;
        }

        public Task DeleteEmployeeAsync(string employeeId)
        {
            _employees.RemoveAll(employee => employee.Id == employeeId);
            return Task.CompletedTask;
        }

        public Task<Employee?> GetEmployeeByIdAsync(string id) =>
            Task.FromResult(_employees.SingleOrDefault(dept => dept.Id == id));

        public Task<IEnumerable<Employee>> GetEmployeesAsync(SearchCriteria searchCriteria)
        {
            string? term = searchCriteria.SearchTerm;

            var searchResult =
                term == null
                ? _employees
                : (from employee in _employees
                   where employee.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase) ||
                         employee.Email.Contains(term, StringComparison.InvariantCultureIgnoreCase) ||
                         employee.Department.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase)
                   select employee);

            return Task.FromResult(searchResult.AsEnumerable());
        }

        public Task UpdateEmployeeAsync(Employee employee)
        {
            var match = _employees.Single(e => e.Id == employee.Id);

            match.Name = employee.Name; 
            match.Email = employee.Email;
            match.DateOfBirth = employee.DateOfBirth;
            match.Department = employee.Department;

            return Task.CompletedTask;
        }
    }
}
