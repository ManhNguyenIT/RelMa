using Microsoft.AspNetCore.Diagnostics;
using RelMa.Shared.Exceptions;
using System.Text.Json;

namespace RelMa.ApiService;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        int statusCode = GetStatusCode(exception);
        var response = new
        {
            apiVersion = httpContext.GetRequestedApiVersion()?.ToString(),
            status = statusCode,
            title = GetTitle(exception),
            detail = exception.Message,
            errors = GetErrors(exception),
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken: cancellationToken);

        return true;
    }

    private static int GetStatusCode(Exception exception) => exception switch
    {
        BadRequestException => StatusCodes.Status400BadRequest,
        InvalidException => StatusCodes.Status400BadRequest,
        UnauthorizedException => StatusCodes.Status401Unauthorized,
        NotFoundException => StatusCodes.Status404NotFound,
        ValidationException => StatusCodes.Status400BadRequest,
        FormatException => StatusCodes.Status422UnprocessableEntity,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetTitle(Exception exception) => exception switch
    {
        BaseException applicationException => applicationException.Title,
        _ => "Server Error"
    };

    private static IReadOnlyCollection<ValidationError>? GetErrors(Exception exception) => exception switch
    {
        ValidationException validationException => validationException.Errors,
        _ => null
    };
}

