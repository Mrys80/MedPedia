using System.Security.Claims;
using MedPedia.Common.Utilities.Extensions;
using MedPedia.Common.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;

namespace MedPedia.Common.Utilities.Assistant;

public class AccessTokenHelper
{
    public static int GetRandomNumber(HttpContext httpContext)
    {
        return GetClaim(httpContext, Constants.Constants.RandomNumber).ToInt();
    }

    public static string GetClaim(HttpContext httpContext, string type)
    {
        if (httpContext is null)
            return null;

        return httpContext.User.Claims.SingleOrDefault(x => x.Type == type)?.Value;
    }

    public static CurrentUser GetCurrentUser(HttpContext httpContext)
    {
        return new CurrentUser
        {
            Username = GetClaim(httpContext, ClaimTypes.Name),
            Id = Convert.ToInt32(GetClaim(httpContext, ClaimTypes.NameIdentifier)),
            SessionId = GetClaim(httpContext, Constants.Constants.SessionId)
        };
    }

    public static List<string> GetRoles(HttpContext httpContext)
    {
        return httpContext?.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Type).ToList();
    }

    // public static string GetToken(HttpContext httpContext)
    // {
    //     if (httpContext is null)
    //         return null;
    //
    //     string authHeader = httpContext.Request.Headers["Authorization"];
    //     return (authHeader is not null && authHeader.StartsWith("Bearer")) ? authHeader.Split(" ").Last() : null;
    // }
}