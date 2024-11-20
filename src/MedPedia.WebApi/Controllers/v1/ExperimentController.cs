using MedPedia.Entities.Dto.Classification;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.DrugCategory;
using MedPedia.Entities.Dto.Experiment;
using MedPedia.Entities.Dto.Prescription;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class ExperimentController(IHttpContextAccessor httpContextAccessor, IExperimentService experimentService)
    : BaseController(httpContextAccessor)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllExperiment(
        CancellationToken cancellationToken)
    {
        var result =
            await experimentService.GetAllExperiment(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{categoryId:Int}")]
    public async Task<IActionResult> GetAllExperimentByCategoryId(
        int categoryId,
        CancellationToken cancellationToken
    )
    {
        var result =
            await experimentService.GetAllExperimentByCategoryId(
                categoryId, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddExperiment(
        [FromBody] ExperimentAddDto experimentAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await experimentService.AddExperiment(
                CurrentUser, experimentAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllExperimentCategory(
        CancellationToken cancellationToken)
    {
        var result =
            await experimentService.GetAllExperimentCategory(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddExperimentCategory(
        [FromBody] ExperimentCategoryAddDto experimentCategoryAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await experimentService.AddExperimentCategory(
                CurrentUser, experimentCategoryAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{experimentId:int}")]
    public async Task<IActionResult> GetExperimentInfo(
        int experimentId, CancellationToken cancellationToken)
    {
        var result =
            await experimentService.GetExperimentInfo(experimentId, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{experimentId:int}")]
    public async Task<IActionResult> GetExperimentDetail(
        int experimentId, CancellationToken cancellationToken)
    {
        var result = await experimentService.GetExperimentDetail(experimentId, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}