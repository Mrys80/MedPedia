using Microsoft.IdentityModel.Tokens;

namespace MedPedia.Common.Utilities.Security.Encryption;

public class SigningCredentialsHelper
{
    public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}