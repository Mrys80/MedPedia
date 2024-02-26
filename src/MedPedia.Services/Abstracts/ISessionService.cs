using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Session;

namespace MedPedia.Services.Abstracts;

public interface ISessionService
{
    Task<IDataResult<SessionDto>> Get(Guid id, CancellationToken cancellationToken);
    Task<IDataResult<SessionDto>> Add(SessionAddDto sessionAddDto, CancellationToken cancellationToken);
}