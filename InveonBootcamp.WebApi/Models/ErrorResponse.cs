namespace InveonBootcamp.WebApi.Models
{
    public class ErrorResponse
    {
        public string ErrorType { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class ValidationErrorDetails : ErrorResponse
    {
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }

    public class NotFoundErrorDetails : ErrorResponse
    {
        public string ResourceName { get; set; }
        public int ResourceId { get; set; }
    }

    public class BusinessRuleErrorDetails : ErrorResponse
    {
        public string BusinessRule { get; set; }
    }
}