using MedPedia.WebFramework.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MedPedia.WebFramework.Configuration;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }

    public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorsMiddleware>();
    }

    public static IApplicationBuilder UseStringSanitizer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StringSanitizerMiddleware>();
    }
}
