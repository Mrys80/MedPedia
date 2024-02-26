using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Common.Utilities.Security.Jwt;

public class AccessToken : IDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}