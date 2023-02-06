using EmployeeManager.Application.Exceptions;
using System.Net;

namespace EmployeeManager.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorCode =
                    ex switch
                    {
                        EntityNotFoundException => HttpStatusCode.NotFound,
                        LogicalValidationException => HttpStatusCode.BadRequest,
                        FluentValidation.ValidationException => HttpStatusCode.BadRequest,
                        _ => HttpStatusCode.InternalServerError
                    };


                context.Response.StatusCode = (int)errorCode;

                await context.Response.WriteAsJsonAsync(new
                {
                    ErrorCode = errorCode,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
