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
public class ExperimentController : BaseController
{
    private readonly IExperimentService _experimentService;


    public ExperimentController(IHttpContextAccessor httpContextAccessor, IExperimentService experimentService) : base(
        httpContextAccessor)
    {
        _experimentService = experimentService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllExperiment(
        CancellationToken cancellationToken)
    {
        var result =
            await _experimentService.GetAllExperiment(cancellationToken);

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
            await _experimentService.GetAllExperimentByCategoryId(
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
            await _experimentService.AddExperiment(
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
            await _experimentService.GetAllExperimentCategory(cancellationToken);

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
            await _experimentService.AddExperimentCategory(
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
            await _experimentService.GetExperimentInfo(experimentId, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}