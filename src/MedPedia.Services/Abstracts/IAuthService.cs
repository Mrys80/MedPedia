using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;

namespace MedPedia.Services.Abstracts;

public interface IAuthService
{

    Task<IResult> Logout(CurrentUserDto currentUser, CancellationToken cancellationToken);
}