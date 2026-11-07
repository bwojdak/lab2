using System.Diagnostics;

namespace lab2.Middleware;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _log;

    public RequestTimingMiddleware(
        RequestDelegate next,
        ILogger<RequestTimingMiddleware> log)
    {
        _next = next;
        _log = log;
    }

    public async Task InvokeAsync(HttpContext ctx, RequestStats stats)
    {
        // Blokowanie wybranej ścieżki
        if (ctx.Request.Path.StartsWithSegments("/blokada"))
        {
            ctx.Response.StatusCode = 403;
            return;
        }

        var sw = Stopwatch.StartNew();
        var n = stats.Hit();

        ctx.Response.OnStarting(() =>
        {
            ctx.Response.Headers["X-Elapsed-Ms"] =
                sw.ElapsedMilliseconds.ToString();

            ctx.Response.Headers["X-Request-Count"] = n.ToString();

            return Task.CompletedTask;
        });

        await _next(ctx);

        sw.Stop();

        _log.LogInformation(
            "{Method} {Path} -> {Status} w {Ms} ms",
            ctx.Request.Method,
            ctx.Request.Path,
            ctx.Response.StatusCode,
            sw.ElapsedMilliseconds);
    }
}