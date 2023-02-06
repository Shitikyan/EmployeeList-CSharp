using EmployeeManager.Application.Commands.CreateEmployee;
using EmployeeManager.Application.Commands.DeleteEmployee;
using EmployeeManager.Application.Commands.UpdateEmployee;
using EmployeeManager.Application.Models;
using EmployeeManager.Application.Queries.GetEmployees;
using EmployeeManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Employee> Create([FromBody] CreateEmployeeCommand command) =>
            await _mediator.Send(command);

        [HttpGet]
        public async Task<IEnumerable<EmployeeModel>> Get([FromQuery] string? searchTerm)
        {
            return await _mediator.Send(new GetEmployeesQuery
            {
                SearchTerm = searchTerm
            });
        }

        [HttpPut("{id}")]
        public async Task Update([FromRoute] string id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            await _mediator.Send(new DeleteEmployeeCommand
            {
                EmployeeId = id
            });
        }
    }
}