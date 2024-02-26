using MedPedia.Entities.Dto.Auth;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teb.WebApi.Controllers.v1;

[ApiVersion("1")]
public class AuthController : BaseSimpleController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
}