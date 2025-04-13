using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions globally in the application.
    /// Captures unhandled exceptions, logs them, and returns a standardized error response.
    /// </summary>
    public class GenericExceptionMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Processes the HTTP request and handles any exceptions that occur during the pipeline execution.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception by setting the response status code, content type, and body.
        /// Logs the exception details and returns a standardized error response in JSON format.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="ex">The exception that occurred.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var error = new ValidationErrorDetail
            {
                Type = "InternalServerError",
                Error = "Internal Server Error",
                Detail = "An unexpected error occurred while processing your request."
            };

            var response = new ApiResponse
            {
                Success = false,
                Message = "An error occurred. Please try again later.",
                Errors = new[] { error }
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Log.Error(ex, "An unexpected error occurred: {Message}, Type: {Type}, Error: {Error}, Path: {Path}", ex.Message, error.Type, error.Error, context.Request.Path);

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
