using MedPedia.Entities.Dto.PrescriptionWriting;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class PrescriptionWritingController(
        IHttpContextAccessor httpContextAccessor,
        IPrescriptionWritingService prescriptionWritingService)
        : BaseController(httpContextAccessor)
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPrescriptions(CancellationToken cancellationToken)
        {
            var result = await prescriptionWritingService.GetAllPrescriptions(cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("[action]/{prescriptionId:int}")]
        public async Task<IActionResult> GetPrescriptionDetail(int prescriptionId, CancellationToken cancellationToken)
        {
            var result = await prescriptionWritingService.GetPrescriptionDetail(prescriptionId, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionWritingAddDto prescriptionDto,
            CancellationToken cancellationToken)
        {
            var result =
                await prescriptionWritingService.AddPrescription(CurrentUser, prescriptionDto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            var result = await prescriptionWritingService.GetAllCategories(cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory([FromBody] PrescriptionWritingCategoryAddDto addDto, CancellationToken cancellationToken)
        {
            var result = await prescriptionWritingService.AddCategory(CurrentUser, addDto, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("[action]/{diseaseId:int}")]
        public async Task<IActionResult> GetPrescriptionsByDiseaseId(int diseaseId, CancellationToken cancellationToken)
        {
            var result = await prescriptionWritingService.GetPrescriptionsByDiseaseId(diseaseId, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchPrescriptions([FromQuery] string text,
            CancellationToken cancellationToken)
        {
            var result = await prescriptionWritingService.SearchPrescriptions(text, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}