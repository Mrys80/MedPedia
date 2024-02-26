using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedPedia.Common.Utilities.Extensions;

public static class ClaimExtensions
{
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        claims.Add(new(JwtRegisteredClaimNames.Email, email));
    }

    public static void AddUsername(this ICollection<Claim> claims, string username)
    {
        claims.Add(new(ClaimTypes.Name, username));
    }
    public static void AddSessionId(this ICollection<Claim> claims, string sessionId)
    {
        claims.Add(new(Constants.Constants.SessionId, sessionId));
    }

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new(ClaimTypes.NameIdentifier, nameIdentifier));
    }

    public static void AddRoles(this ICollection<Claim> claims, string[] roles)
    {
        roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
    }
}