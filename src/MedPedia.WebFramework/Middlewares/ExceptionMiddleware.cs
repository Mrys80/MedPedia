using System.Net;
using FluentValidation;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Extensions;
using MedPedia.Common.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace MedPedia.WebFramework.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;


    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _handleExceptionAsync(httpContext, ex);
        }
    }

    private Task _handleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        Console.WriteLine(exception);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = _setResult(Messages.ServerError);

        object message;

        try
        {
            var path = string.Concat(
                httpContext?.Request.Scheme,
                "://",
                httpContext?.Request.Host.ToUriComponent(),
                httpContext?.Request.PathBase.ToUriComponent(),
                httpContext?.Request.Path.ToUriComponent(),
                httpContext?.Request.QueryString.ToUriComponent()
            );
            // message = new
            // {
            //     exception.Message,
            //     //JWTToken = GetToken(httpContext) == null ? 0 : 1,
            //     Username = GetClaim(httpContext, Constants.Name),
            //     IP = httpContext.Connection.RemoteIpAddress?.ToString(),
            //     Path = path,
            //     //stackTrace = exception.StackTrace,
            //     timestamp = DateTime.Now
            // };
            // var errorLog = new ErrorLog
            // {
            //     Message = exception.Message,
            //     JWTToken = GetToken(httpContext),
            //     Username = GetClaim(httpContext, Constants.Name),
            //     IP = httpContext.Connection.RemoteIpAddress?.ToString(),
            //     Path = path,
            //     StackTrace = exception.StackTrace,
            //     Timestamp = DateTime.Now
            // };
            // _logService.RegisterErrorLog(errorLog);
        }
        catch (Exception)
        {
            message = new { result };
        }

        // _logger.Error(JsonConvert.SerializeObject(message));

        _getValidationException(ref httpContext, exception, ref result);
        _getAuthException(ref httpContext, exception, ref result);

        return httpContext.Response.WriteAsync(result);
    }

    private void _getValidationException(ref HttpContext httpContext, Exception e, ref string result)
    {
        if (e.GetType() != typeof(ValidationException))
            return;
        
        // TODO :: FluentValidation only

        var exception = (ValidationException)e;

        var message = exception.Message.Replace("\\r\\n", "").Split("--").Select(x => x.Trim()).ToList()[1];

        var messageId = (Messages)new string(message.Where(char.IsDigit).ToArray()).ToInt();

        httpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;


        _setResult(messageId, ref result);
    }

    private void _getAuthException(ref HttpContext httpContext, Exception e, ref string result)
    {
        if (e.GetType() != typeof(UnauthorizedAccessException)) //AuthException
            return;

        var exception = (UnauthorizedAccessException)e; //AuthException

        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        _setResult(Messages.Unauthorized, ref result);
    }

    private void _setResult(Messages messageId, ref string result)
    {
        result = _setResult(messageId);
    }

    private string _setResult(Messages messageId)
    {
        return new ErrorResult(messageId).ToString();
    }
}