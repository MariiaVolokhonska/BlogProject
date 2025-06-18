using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Microservices.Comments.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await ProduceErrorResponce(context, ex);

            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await ProduceErrorResponce(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await ProduceErrorResponce(context, ex);
            }
        }
        private async Task ProduceErrorResponce(HttpContext context,Exception ex)
        {
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { error = ex.Message });
            await context.Response.WriteAsync(result);
        }
    }
}

