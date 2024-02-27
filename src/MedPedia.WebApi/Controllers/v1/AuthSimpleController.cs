using MedPedia.Entities.Dto.Auth;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class AuthSimpleController : BaseSimpleController
{
    private readonly IAuthSimpleService _authSimpleService;

    public AuthSimpleController(IAuthSimpleService authSimpleService)
    {
        _authSimpleService = authSimpleService;
    }

    
    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> OtpLogin([FromBody] OtpDto otpDto, CancellationToken cancellationToken)
    {
        var result = await _authSimpleService.OtpLogin(otpDto, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UserSignUp([FromQuery] string phoneNumber, CancellationToken cancellationToken)
    {
        var result = await _authSimpleService.UserSignUp(phoneNumber, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}