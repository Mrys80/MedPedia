using MedPedia.Common.Entities.Dto;
using MedPedia.Common.Entities.Entities;

namespace MedPedia.Common.Utilities.Security.Jwt;

public interface ITokenHelper
{
    CurrentUser CurrentUser { get; }
    AccessToken CreateToken(User user, IEnumerable<RoleDto> roles, string sessionId);
}