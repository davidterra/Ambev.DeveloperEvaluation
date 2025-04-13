using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions of type <see cref="ValidationException"/> thrown during the request pipeline.
    /// Captures validation errors and returns a structured JSON response with error details.
    /// </summary>
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the request pipeline.</param>
        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to process the HTTP request.
        /// Catches <see cref="ValidationException"/> and handles it by returning a 400 Bad Request response.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the <see cref="ValidationException"/> by constructing a JSON response with validation error details.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The validation exception containing error details.</param>
        /// <returns>A task that represents the asynchronous operation of writing the response.</returns>
        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Validation Failed",
                Errors = exception.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Log.Information("Validation error: {Message}, Errors: {@Errors}, Path: {Path}", exception.Message, exception.Errors, context.Request.Path);

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
