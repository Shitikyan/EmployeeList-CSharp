using EmployeeManager.Application.Abstractions;
using EmployeeManager.Application.Commands.CreateEmployee;
using EmployeeManager.Application.Exceptions;
using EmployeeManager.Domain.Entities;
using Moq;
using Xunit;

namespace EmployeeManager.Tests
{
    public class CreateEmployeeCommandTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
        private readonly CreateEmployeeCommandHandler _systemUnderTest;

        public CreateEmployeeCommandTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();

            _systemUnderTest = new CreateEmployeeCommandHandler(_employeeRepositoryMock.Object, _departmentRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateEmployeeCommand_WhenCommandValid_PassesCorrrectObjectToRepo()
        {
            var newEmployee = new CreateEmployeeCommand {

                Name = "sneaky",
                Email = "very@sneaky.com",
                DateOfBirth = DateTime.Today,
                DepartmentId = "1"
            };

            _departmentRepositoryMock.Setup(repo => repo.GetDepartmentByIdAsync("1"))
                .ReturnsAsync(new Department
                {
                    Id = "1",
                    Name = "Accounting"
                });

            await _systemUnderTest.Handle(newEmployee, CancellationToken.None);

            _employeeRepositoryMock.Verify(
                repo => 
                repo.CreateEmployeeAsync(
                    It.Is<Employee>(x =>
                    x.Name == newEmployee.Name &&
                    x.Email == newEmployee.Email &&
                    x.DateOfBirth == newEmployee.DateOfBirth &&
                    x.Department.Id == newEmployee.DepartmentId &&
                    x.Department.Name == "Accounting")), Times.Once());
        }

        [Fact]
        public async Task CreateEmployeeCommand_WhenDepartmentDoesNotExist_ThrowsLogicalException()
        {
            var newEmployee = new CreateEmployeeCommand
            {

                Name = "quite sneaky",
                Email = "very@sneaky.com",
                DateOfBirth = DateTime.Today,
                DepartmentId = "2"
            };

            await Assert.ThrowsAsync<LogicalValidationException>(
                async () => await _systemUnderTest.Handle(newEmployee, CancellationToken.None));

        }
    }
}
