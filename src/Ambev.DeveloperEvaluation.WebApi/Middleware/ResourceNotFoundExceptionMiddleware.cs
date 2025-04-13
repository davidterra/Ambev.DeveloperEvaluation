using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Serilog;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    /// <summary>
    /// Middleware to handle ResourceNotFoundException and return a standardized error response.
    /// </summary>
    public class ResourceNotFoundExceptionMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// Processes the HTTP request and catches ResourceNotFoundException to handle it.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ResourceNotFoundException re)
            {
                await HandleResourceNotFoundExceptionAsync(context, re);
            }
        }

        /// <summary>
        /// Handles the ResourceNotFoundException by setting the response status code, content type, 
        /// and returning a JSON-formatted error response.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="re">The caught ResourceNotFoundException instance.</param>
        /// <returns>A task representing the asynchronous operation of writing the response.</returns>
        private static Task HandleResourceNotFoundExceptionAsync(HttpContext context, ResourceNotFoundException re)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var error = new ValidationErrorDetail { Type = re.Type, Error = re.Error, Detail = re.Message };

            var response = new ApiResponse
            {
                Success = false,
                Message = "Resource not found",
                Errors = [error]
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Log.Error(re, "Resource not found: {Message}, Type: {Type}, Error: {Error}, Path: {Path}", re.Message, re.Type, re.Error, context.Request.Path);

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
