using System.Diagnostics;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            
            await _next(context);

            sw.Stop();
            var statusCode = context.Response.StatusCode;
            var method = context.Request.Method;
            var path = context.Request.Path;
            var elapsedMilliseconds = sw.ElapsedMilliseconds;

            _logger.LogInformation("{Method} {Path} - {StatusCode} - {ElapsedMilliseconds}ms", 
                method, path, statusCode, elapsedMilliseconds);
        }
    }
}
