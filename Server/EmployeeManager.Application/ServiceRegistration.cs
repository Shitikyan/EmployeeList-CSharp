using EmployeeManager.Application.Behaviors;
using EmployeeManager.Application.Commands.CreateEmployee;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmployeeManager.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services
            .AddValidatorsFromAssemblyContaining(typeof(CreateEmployeeCommandValidator))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddMediatR(Assembly.GetExecutingAssembly());
    }
}
