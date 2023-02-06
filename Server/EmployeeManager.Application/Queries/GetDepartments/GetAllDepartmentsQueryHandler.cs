using EmployeeManager.Application.Abstractions;
using EmployeeManager.Domain.Entities;
using MediatR;

namespace EmployeeManager.Application.Queries.GetDepartments
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken) =>
            await _departmentRepository.GetAllDepartmentsAsync();
    }
}
