using backend.Exceptions;
using backend.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace backend.Api.ExceptionHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");
        var statusCode = exception switch
        {
            AppException appException => appException.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };
        var message = exception switch
        {
            AppException appException => appException.Message,
            _ => "An unexpected server error occurred."
        };
        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Data = null
        };
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}