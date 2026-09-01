using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vault.Services.OnBoarding.Domain.Exceptions;

namespace Vault.Services.OnBoarding.Api.Middlewares
{
    /// <summary>Turns every unhandled exception into an RFC 9457 ProblemDetails response.</summary>
    internal sealed class GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var known = exception as BaseException;
            var status = known?.StatusCode ?? StatusCodes.Status500InternalServerError;
            var title = known?.Title ?? "Internal server error";

            if (status >= StatusCodes.Status500InternalServerError)
            {
                logger.LogError(exception, "Unhandled exception for {Method} {Path}",
                    httpContext.Request.Method, httpContext.Request.Path);
            }
            else
            {
                logger.LogInformation("{Method} {Path} refused with {Status}: {Message}",
                    httpContext.Request.Method, httpContext.Request.Path, status, exception.Message);
            }

            httpContext.Response.StatusCode = status;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = status,
                    Title = title,
                    Detail = status >= StatusCodes.Status500InternalServerError ? "An unexpected error occurred." : exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    Extensions = { ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier }
                }
            });
        }
    }
}
