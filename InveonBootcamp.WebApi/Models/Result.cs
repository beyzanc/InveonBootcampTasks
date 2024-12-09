using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.WebApi.Models
{
    public class Result
    {
        public int Status { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T data, int status)
        {
            return new Result<T>
            {
                Data = data,
                Status = status
            };
        }

        public static Result<T> Fail(string message, int status = 400)
        {
            return new Result<T>
            {
                Status = status,
                ProblemDetails = new ProblemDetails
                {
                    Detail = message,
                    Status = status
                }
            };
        }
    }
}
