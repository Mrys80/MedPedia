using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.DrugCategory;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class DrugController : BaseController
{
    private readonly IDrugService _drugService;
    private readonly IDrugCategoryService _drugCategoryService;


    public DrugController(IHttpContextAccessor httpContextAccessor, IDrugService drugService,
        IDrugCategoryService drugCategoryService) : base(httpContextAccessor)
    {
        _drugService = drugService;
        _drugCategoryService = drugCategoryService;
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDrug(
        CancellationToken cancellationToken)
    {
        var result =
            await _drugService.GetAllDrug(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{drugCategoryId:Int}")]
    public async Task<IActionResult> GetAllDrugByCategoryId(
        int drugCategoryId,
        CancellationToken cancellationToken
    )
    {
        var result =
            await _drugService.GetAllDrugByCategoryId(
                drugCategoryId, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddDrug(
        [FromBody] DrugAddDto drugAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await _drugService.AddDrug(
                CurrentUser, drugAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllDrugCategories(
        CancellationToken cancellationToken)
    {
        var result =
            await _drugCategoryService.GetAllDrugCategories(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddDrugCategories(
        [FromBody]  DrugCategoryAddDto drugCategoryAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await _drugCategoryService.AddDrugCategories(
                CurrentUser, drugCategoryAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> SearchDrug([FromQuery] string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var result = await _drugService.SearchDrug(text, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SearchDrugCategories([FromQuery] string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var result = await _drugCategoryService.SearchDrugCategories(text, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}