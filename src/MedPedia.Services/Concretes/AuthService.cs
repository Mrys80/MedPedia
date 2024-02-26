using MedPedia.Common.Abstracts;
using MedPedia.Common.Entities.Entities;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Common.Utilities.Security.Jwt;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class AuthService : IAuthService, IScopedDependency
{
    private readonly IRepository<User> _userRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly ISessionService _sessionService;
    private readonly IRepository<Session> _sessionRepository;
    private readonly IRepository<Role> _roleRepository;


    public AuthService(IRepository<User> userRepository, ITokenHelper tokenHelper,
        ISessionService sessionService, IRepository<Session> sessionRepository, IRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _sessionService = sessionService;
        _sessionRepository = sessionRepository;
        _roleRepository = roleRepository;
    }


    
    public async Task<IResult> Logout(CurrentUserDto currentUser, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(x =>
            x.Id == currentUser.UserId && x.IsDeleted == false, cancellationToken);

        if (user == null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        var session =
            await _sessionRepository.GetAsync(x => x.Id == Guid.Parse(currentUser.SessionId), cancellationToken);

        if (session != null)
        {
            session.IsDeleted = true;
            await _sessionRepository.UpdateAsync(session, cancellationToken);
        }

        return new SuccessResult();
    }


}