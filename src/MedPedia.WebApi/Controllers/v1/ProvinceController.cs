using MedPedia.Entities.Entities;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]

public class ProvinceController(
    IHttpContextAccessor httpContextAccessor, IProvinceService provinceService, ICityService cityService) : BaseController (httpContextAccessor)
{
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllProvinces(CancellationToken cancellationToken)
    {
        var result = await provinceService.GetAllProvinces(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("[action]/{provinceId:int}")]
    public async Task<IActionResult> GetAllCities(int provinceId,CancellationToken cancellationToken)
    {
        var result = await cityService.GetAllCities(provinceId,cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
}