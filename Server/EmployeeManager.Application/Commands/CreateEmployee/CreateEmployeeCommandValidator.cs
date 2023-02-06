using FluentValidation;

namespace EmployeeManager.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(employee => employee.Name).NotEmpty().MaximumLength(100);
            RuleFor(employee => employee.Email).NotEmpty().EmailAddress();
            RuleFor(employee => employee.DepartmentId).NotEmpty().MaximumLength(200);
        }
    }
}
