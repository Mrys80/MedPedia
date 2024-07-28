using MedPedia.Common.Entities.Dto;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Prescription;

namespace MedPedia.Services.Abstracts;

public interface IUserService
{
    Task<IDataResult<UserDto>> GetUserInfo(CurrentUserDto currentUser, CancellationToken cancellationToken);
    Task<IDataResult<UserDto>> SetUserInfo(AddUserInfoDto addUserInfoDto,CurrentUserDto currentUser, CancellationToken cancellationToken);
}