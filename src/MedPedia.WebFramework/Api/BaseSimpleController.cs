using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebFramework.Api;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseSimpleController : ControllerBase
{
}