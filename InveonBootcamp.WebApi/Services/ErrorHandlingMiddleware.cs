using System.Net;
using InveonBootcamp.WebApi.Models;
using System.Text.Json;

namespace InveonBootcamp.WebApi.Services
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse
            {
                ErrorType = exception.GetType().Name,
                Message = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            var result = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(result);
        }
    }
}