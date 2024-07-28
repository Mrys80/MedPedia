using MedPedia.Common.Entities.Dto;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class UserController(IHttpContextAccessor httpContextAccessor, IUserService userService)
    : BaseController(httpContextAccessor)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserInfo(
        CancellationToken cancellationToken)
    {
        var result =
            await userService.GetUserInfo(CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> SetUserInfo(
        [FromBody] AddUserInfoDto addUserInfoDto,
        CancellationToken cancellationToken
    )
    {
        var result =
            await userService.SetUserInfo(
                addUserInfoDto, CurrentUser, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}