using MediatR;
using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string DepartmentId { get; set; }
    }
}
