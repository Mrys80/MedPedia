using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Entities.Dto;

namespace MedPedia.Entities.Dto.Auth;

public class 
LoginResultDto : IDto
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<RoleDto> roles { get; set; }
}