using EmployeeManager.Application.Models;
using MediatR;

namespace EmployeeManager.Application.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeModel>>
    {
        public string? SearchTerm { get; set; }
    }
}
 