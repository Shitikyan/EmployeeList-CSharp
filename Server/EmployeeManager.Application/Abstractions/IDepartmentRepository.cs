using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Abstractions
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();

        Task<Department?> GetDepartmentByIdAsync(string id);
    }
}
