using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Auth;

public class LoginDto : IDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}