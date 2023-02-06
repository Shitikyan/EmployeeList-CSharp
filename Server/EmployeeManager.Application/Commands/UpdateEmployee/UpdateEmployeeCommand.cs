using MediatR;

namespace EmployeeManager.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public string? Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string DepartmentId { get; set; }
    }
}
