using MedPedia.Entities.Dto.Classification;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.DrugCategory;
using MedPedia.Entities.Dto.Prescription;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class PrescriptionController : BaseController
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly IClassificationService _classificationService;


    public PrescriptionController(IHttpContextAccessor httpContextAccessor, IPrescriptionService prescriptionService,
        IClassificationService classificationService) : base(httpContextAccessor)
    {
        _prescriptionService = prescriptionService;
        _classificationService = classificationService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllPrescription(
        CancellationToken cancellationToken)
    {
        var result =
            await _prescriptionService.GetAllPrescription(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{classificationId:Int}")]
    public async Task<IActionResult> GetAllPrescriptionByClassificationId(
        int classificationId,
        CancellationToken cancellationToken
    )
    {
        var result =
            await _prescriptionService.GetAllPrescriptionByClassificationId(
                classificationId, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddPrescription(
        PrescriptionAddDto prescriptionAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await _prescriptionService.AddPrescription(
                CurrentUser, prescriptionAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllClassification(
        CancellationToken cancellationToken)
    {
        var result =
            await _classificationService.GetAllClassification(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddClassification(
        ClassificationAddDto classificationAddDto, CancellationToken cancellationToken
    )
    {
        var result =
            await _classificationService.AddClassification(
                CurrentUser, classificationAddDto, cancellationToken
            );

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> SearchPrescription([FromQuery] string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var result = await _prescriptionService.SearchPrescription(text, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SearchClassification([FromQuery] string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return BadRequest("Search text cannot be empty.");
        }

        var result = await _classificationService.SearchClassification(text, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}