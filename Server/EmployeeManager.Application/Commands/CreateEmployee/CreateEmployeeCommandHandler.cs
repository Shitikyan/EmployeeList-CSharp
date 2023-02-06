using EmployeeManager.Application.Abstractions;
using EmployeeManager.Application.Exceptions;
using EmployeeManager.Domain.Entities;
using MediatR;

namespace EmployeeManager.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(command.DepartmentId);

            if (department == null)
            {
                throw new LogicalValidationException("Department not found");
            }

            var employee = new Employee
            {
                Name = command.Name,
                Email = command.Email,
                DateOfBirth = command.DateOfBirth,
                Department = department
            };

            await _employeeRepository.CreateEmployeeAsync(employee);

            return employee;
        }
    }
}
