using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Common.Utilities.Settings;
using MedPedia.Entities.Dto.Session;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.Extensions.Options;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class SessionService : ISessionService, IScopedDependency
{
    private readonly IRepository<Session> _sessionRepository;
    private readonly JwtSetting _jwtSetting;

    public SessionService(IRepository<Session> sessionRepository, IOptionsSnapshot<Setting> setting)
    {
        _sessionRepository = sessionRepository;
        _jwtSetting = setting.Value.JwtSetting;
    }

    public async Task<IDataResult<SessionDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetAsync(s => s.Id == id, cancellationToken);

        if (session is null)
        {
            return new ErrorDataResult<SessionDto>(Messages.SessionNotFound);
        }

        var sessionDto = session.Adapt<SessionDto>();

        return new SuccessDataResult<SessionDto>(sessionDto);
    }

    public async Task<IDataResult<SessionDto>> Add(SessionAddDto sessionAddDto, CancellationToken cancellationToken)
    {
        var session = new Session
        {
            IsDeleted = false,
            RegUserId = sessionAddDto.RegUserId,
            FromDate = DateTime.Now,
            ToDate = DateTime.Now.AddMinutes(_jwtSetting.ExpirationMinutes),
            RegDate = DateTime.Now
        };

        await _sessionRepository.AddAsync(session, cancellationToken);

        return await Get(session.Id, cancellationToken);
    }
}