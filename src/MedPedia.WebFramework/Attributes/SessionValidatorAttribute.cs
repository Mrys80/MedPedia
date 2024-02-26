using MedPedia.Common.Entities.Entities;
using MedPedia.Common.Utilities.Assistant;
using MedPedia.Common.Utilities.Security.Jwt;
using MedPedia.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Teb.Data.Abstracts;

namespace MedPedia.WebFramework.Attributes;

public class SessionValidatorAttribute : ActionFilterAttribute
{
    private readonly IRepository<Session> _sessionRepository;
    private readonly IRepository<User> _userRepository;

    public SessionValidatorAttribute(IRepository<Session> sessionRepository, IRepository<User> userRepository)
    {
        _sessionRepository = sessionRepository;
        _userRepository = userRepository;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var currentUser = AccessTokenHelper.GetCurrentUser(context.HttpContext);

        if (await IsEnable(currentUser) is false || await HasSession(currentUser) is false)
        {
            throw new UnauthorizedAccessException();
        }

        await base.OnActionExecutionAsync(context, next);
    }

    private async Task<bool> IsEnable(CurrentUser currentUser)
    {
        var user = await _userRepository.GetAsync(x => x.PhoneNumber == currentUser.Username && x.IsDeleted == false);
        return user != null ;
    }

    private async Task<bool> HasSession(CurrentUser currentUser)
    {
        var session = await _sessionRepository.GetAsync(x =>
            x.Id == Guid.Parse(currentUser.SessionId) && x.IsDeleted == false && x.RegUserId == currentUser.Id);


        return session != null && DateTime.Now >= session.FromDate && DateTime.Now <= session.ToDate;
    }
}