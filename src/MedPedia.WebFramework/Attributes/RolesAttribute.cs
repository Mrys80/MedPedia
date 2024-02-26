using Microsoft.AspNetCore.Authorization;

namespace MedPedia.WebFramework.Attributes;

public class RolesAttribute : AuthorizeAttribute
{
    public RolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}