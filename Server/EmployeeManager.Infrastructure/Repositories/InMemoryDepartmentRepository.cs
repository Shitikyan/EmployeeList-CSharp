using EmployeeManager.Application.Abstractions;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Infrastructure.Repositories
{
    public class InMemoryDepartmentRepository : IDepartmentRepository
    {
        private readonly List<Department> _departments = new List<Department>() {
            new Department
            {
                Id = "1",
                Name = "Accounting"
            },
            new Department
            {
                Id = "2",
                Name = "R&D"
            },
            new Department
            {
                Id = "3",
                Name = "HR"
            }
        };

        public Task<IEnumerable<Department>> GetAllDepartmentsAsync() =>
            Task.FromResult(_departments.AsEnumerable());

        public Task<Department?> GetDepartmentByIdAsync(string id) =>
             Task.FromResult(_departments.SingleOrDefault(dept => dept.Id == id));
    }
}
