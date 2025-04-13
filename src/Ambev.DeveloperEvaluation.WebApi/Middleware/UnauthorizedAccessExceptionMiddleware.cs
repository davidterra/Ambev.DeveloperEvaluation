using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    /// <summary>
    /// Middleware to handle UnauthorizedAccessException and return a standardized JSON response.
    /// </summary>
    public class UnauthorizedAccessExceptionMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Processes the HTTP request and catches UnauthorizedAccessException to handle it.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (UnauthorizedAccessException ue)
            {
                await HandleUnauthorizedAccessExceptionAsync(context, ue);
            }
        }

        /// <summary>
        /// Handles UnauthorizedAccessException by setting the response status code, content type, and body.
        /// Logs the exception details using Serilog.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="ue">The UnauthorizedAccessException instance.</param>
        /// <returns>A task that represents the asynchronous operation of writing the response.</returns>
        private static Task HandleUnauthorizedAccessExceptionAsync(HttpContext context, UnauthorizedAccessException ue)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var error = new ValidationErrorDetail { Type = "UnauthorizedAccess", Error = "Unauthorized Access", Detail = ue.Message };

            var response = new ApiResponse
            {
                Success = false,
                Message = "Unauthorized Access",
                Errors = [error]
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Log.Error(ue, "Unauthorized access: {Message}, Type: {Type}, Error: {Error}, Path: {Path}", ue.Message, error.Type, error.Error, context.Request.Path);

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
