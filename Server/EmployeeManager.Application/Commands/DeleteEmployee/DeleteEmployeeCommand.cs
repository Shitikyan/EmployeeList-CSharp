using MediatR;

namespace EmployeeManager.Application.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public string EmployeeId { get; set; }
    }
}
