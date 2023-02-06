using EmployeeManager.Application.Abstractions;
using EmployeeManager.Application.Exceptions;
using EmployeeManager.Domain.Entities;
using MediatR;

namespace EmployeeManager.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(command.Id!);

            if (existingEmployee == null)
            {
                throw new EntityNotFoundException("Employee not found");
            }

            var department = await _departmentRepository.GetDepartmentByIdAsync(command.DepartmentId);

            if (department == null)
            {
                throw new LogicalValidationException("Department not found");
            }

            existingEmployee.Name = command.Name;
            existingEmployee.Email = command.Email;
            existingEmployee.DateOfBirth = command.DateOfBirth;
            existingEmployee.Department = department;

            await _employeeRepository.UpdateEmployeeAsync(existingEmployee);

            return Unit.Value;
        }
    }
}
