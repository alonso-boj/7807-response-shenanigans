using Microsoft.AspNetCore.Mvc;

namespace Company.Store.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Type = $"https://httpstatuses.com/{StatusCodes.Status500InternalServerError}",
                Title = ex.Message,
                Detail = "See the logs for more information.",
                Instance = context.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
            };

            problemDetails.Extensions.Add("stackTrace", ex.StackTrace);

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
