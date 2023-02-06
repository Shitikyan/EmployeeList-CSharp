using EmployeeManager.Application.Commands.CreateEmployee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(employee => employee.Id).NotEmpty().MaximumLength(200);
            RuleFor(employee => employee.Name).NotEmpty().MaximumLength(100);
            RuleFor(employee => employee.Email).NotEmpty().EmailAddress();
            RuleFor(employee => employee.DepartmentId).NotEmpty().MaximumLength(200);
        }
    }
}
