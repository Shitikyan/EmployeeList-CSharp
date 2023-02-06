using EmployeeManager.Application.Abstractions;
using EmployeeManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManager.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) =>
            services
            .AddSingleton<IEmployeeRepository, InMemoryEmployeeRepository>()
            .AddSingleton<IDepartmentRepository, InMemoryDepartmentRepository>();
    }
}
