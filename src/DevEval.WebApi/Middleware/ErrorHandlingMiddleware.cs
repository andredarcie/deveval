using System.Net;
using System.Text.Json;

namespace DevEval.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorType = exception switch
            {
                KeyNotFoundException => "ResourceNotFound",
                UnauthorizedAccessException => "AuthenticationError",
                ArgumentException or ArgumentNullException => "ValidationError",
                _ => "InternalServerError"
            };

            var response = new
            {
                type = errorType,
                error = errorType switch
                {
                    "ResourceNotFound" => "Resource not found",
                    "AuthenticationError" => "Authentication error",
                    "ValidationError" => "Validation error",
                    _ => "Internal server error"
                },
                detail = exception.Message
            };

            context.Response.StatusCode = errorType switch
            {
                "ResourceNotFound" => (int)HttpStatusCode.NotFound, // 404
                "AuthenticationError" => (int)HttpStatusCode.Unauthorized, // 401
                "ValidationError" => (int)HttpStatusCode.BadRequest, // 400
                _ => (int)HttpStatusCode.InternalServerError // 500
            };

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

    }
}