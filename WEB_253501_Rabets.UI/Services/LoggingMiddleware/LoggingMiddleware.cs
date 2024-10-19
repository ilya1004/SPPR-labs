using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WEB_253501_Rabets.UI.Services.LoggingMiddleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        if (context.Response.StatusCode >= 400)
        {
            _logger.LogError($"{DateTime.UtcNow} ---> request {context.Request.Path} returns {context.Response.StatusCode}");
        }
        else
        {
            _logger.LogInformation($"{DateTime.UtcNow} ---> request {context.Request.Path} returns {context.Response.StatusCode}");
        }
    }
}