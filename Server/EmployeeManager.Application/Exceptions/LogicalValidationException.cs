namespace EmployeeManager.Application.Exceptions
{
    public class LogicalValidationException : Exception
    {
        public LogicalValidationException() { }

        public LogicalValidationException(string message) : base(message) { }
    }
}
