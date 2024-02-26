

using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Auth;

namespace MedPedia.Services.Abstracts;

public interface IAuthSimpleService
{

    Task<IDataResult<LoginResultDto>> OtpLogin(OtpDto otpDto, CancellationToken cancellationToken);

    Task<IResult> UserSignUp(string phoneNumber, CancellationToken cancellationToken);

}