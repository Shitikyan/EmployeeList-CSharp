using EmployeeManager.Application.Abstractions;
using EmployeeManager.Application.Models;
using EmployeeManager.Application.Queries.GetEmployees;
using EmployeeManager.Domain.Entities;
using Moq;
using Xunit;

namespace EmployeeManager.Tests
{
    public class GetEmployeesQueryTests
    {
        private GetEmployeesQueryHandler _systemUnderTest;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;

        public GetEmployeesQueryTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();

            _systemUnderTest = new GetEmployeesQueryHandler(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetEmployeesQuery_ReturnsCorrectlyMappedResults()
        {
            string searchTerm = "trm";
            var employeeData = new List<Employee>()
            {
                new Employee
                {
                    Id = "1",
                    Name = "sneaky",
                    Email = "very@sneaky.com",
                    DateOfBirth = DateTime.Today,
                    Department = new Department
                    {
                        Id = "1",
                        Name = "Accounting"
                    }
                },
                new Employee
                {
                    Id = "2",
                    Name = "loyal",
                    Email = "very@loyal.com",
                    DateOfBirth = DateTime.Today,
                    Department = new Department
                    {
                        Id = "2",
                        Name = "R&D"
                    }
                }
            };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeesAsync(It.Is<SearchCriteria>(x => x.SearchTerm == searchTerm))).ReturnsAsync(
                () => employeeData);

            var employees = await _systemUnderTest.Handle(new GetEmployeesQuery
            {
                SearchTerm = searchTerm,
            }, CancellationToken.None);

            Assert.Collection(employees,
                e =>
                {
                    Assert.Equal(e.Name, employeeData[0].Name);
                    Assert.Equal(e.Email, employeeData[0].Email);
                    Assert.Equal(e.DateOfBirth, employeeData[0].DateOfBirth);
                    Assert.Equal(e.DepartmentId, employeeData[0].Department.Id);
                    Assert.Equal(e.DepartmentName, employeeData[0].Department.Name);

                },
                e =>
                {
                    Assert.Equal(e.Name, employeeData[1].Name);
                    Assert.Equal(e.Email, employeeData[1].Email);
                    Assert.Equal(e.DateOfBirth, employeeData[1].DateOfBirth);
                    Assert.Equal(e.DepartmentId, employeeData[1].Department.Id);
                    Assert.Equal(e.DepartmentName, employeeData[1].Department.Name);
                }
                );
        }
    }
}