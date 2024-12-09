using InveonBootcamp.WebApi.Models;

namespace InveonBootcamp.WebApi.Exceptions
{
    public class ValidationException : Exception
    {
        public int StatusCode { get; private set; }
        public ValidationErrorDetails ErrorDetails { get; private set; }

        public ValidationException(string message, int statusCode = 400)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorDetails = new ValidationErrorDetails
            {
                ErrorType = "ValidationError",
                Message = message
            };
        }
    }

    public class NotFoundException : Exception
    {
        public int StatusCode { get; private set; }
        public NotFoundErrorDetails ErrorDetails { get; private set; }

        public NotFoundException(string resourceName, int id)
            : base($"{resourceName} with id {id} not found.")
        {
            StatusCode = 404;
            ErrorDetails = new NotFoundErrorDetails
            {
                ErrorType = "NotFound",
                ResourceName = resourceName,
                ResourceId = id
            };
        }
    }

    public class BusinessRuleException : Exception
    {
        public int StatusCode { get; private set; }
        public BusinessRuleErrorDetails ErrorDetails { get; private set; }

        public BusinessRuleException(string message, int statusCode = 422)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorDetails = new BusinessRuleErrorDetails
            {
                ErrorType = "BusinessRuleViolation",
                Message = message
            };
        }
    }
}