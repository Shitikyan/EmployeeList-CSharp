using EmployeeManager.Domain.Entities;
using MediatR;

namespace EmployeeManager.Application.Queries.GetDepartments
{
    public class GetAllDepartmentsQuery : IRequest<IEnumerable<Department>>
    {
    }
}
