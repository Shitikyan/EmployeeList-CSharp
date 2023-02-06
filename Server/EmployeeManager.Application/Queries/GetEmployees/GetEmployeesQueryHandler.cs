using EmployeeManager.Application.Abstractions;
using EmployeeManager.Application.Models;
using MediatR;

namespace EmployeeManager.Application.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(new SearchCriteria
            {
                SearchTerm = request.SearchTerm,
            });

            return
                from employee in employees
                select new EmployeeModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    DepartmentId = employee.Department.Id,
                    DepartmentName = employee.Department.Name,
                };
        }
    }
}
