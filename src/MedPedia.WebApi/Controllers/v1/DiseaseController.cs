using MedPedia.Entities.Dto.Disease;
using MedPedia.Entities.Dto.DiseaseCategory;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class DiseaseController : BaseController
{
    private readonly IDiseaseService _diseaseService;
    private readonly IDiseaseCategoryService _diseaseCategoryService;


    public DiseaseController(IHttpContextAccessor httpContextAccessor, IDiseaseService diseaseService,
        IDiseaseCategoryService diseaseCategoryService) : base(
        httpContextAccessor)
    {
        _diseaseService = diseaseService;
        _diseaseCategoryService = diseaseCategoryService;
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDisease(
        CancellationToken cancellationToken)
    {
        var result =
            await _diseaseService.GetAllDisease(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{diseaseCategoryId:Int}")]
    public async Task<IActionResult> GetAllDiseaseByCategoryId(
        int diseaseCategoryId,
        CancellationToken cancellationToken
    )
    {
        var result =
            await _diseaseService.GetAllDiseaseByCategoryId(
                diseaseCategoryId, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddDisease(
        DiseaseAddDto diseaseAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await _diseaseService.AddDisease(
                CurrentUser, diseaseAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDiseaseCategory(
        CancellationToken cancellationToken)
    {
        var result =
            await _diseaseCategoryService.GetAllDiseaseCategory(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddDiseaseCategory(
        DiseaseCategoryAddDto diseaseCategoryAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await _diseaseCategoryService.AddDiseaseCategory(
                CurrentUser, diseaseCategoryAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> SearchDisease([FromQuery]string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var result = await _diseaseService.SearchDisease(text, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SearchDiseaseCategory([FromQuery]string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var result = await _diseaseCategoryService.SearchDiseaseCategory(text, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}