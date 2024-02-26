using Microsoft.AspNetCore.Http;

namespace MedPedia.WebFramework.Middlewares;
public class StringSanitizerMiddleware
{
    private readonly RequestDelegate _next;

    public StringSanitizerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);
    }
}
