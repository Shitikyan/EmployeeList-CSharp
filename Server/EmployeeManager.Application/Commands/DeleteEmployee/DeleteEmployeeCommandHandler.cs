using EmployeeManager.Application.Abstractions;
using MediatR;

namespace EmployeeManager.Application.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteEmployeeAsync(command.EmployeeId);
            return Unit.Value;
        }
    }
}
