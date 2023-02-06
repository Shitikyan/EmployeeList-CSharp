using EmployeeManager.Application.Queries.GetDepartments;
using EmployeeManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            return await _mediator.Send(new GetAllDepartmentsQuery());
        }
    }
}
