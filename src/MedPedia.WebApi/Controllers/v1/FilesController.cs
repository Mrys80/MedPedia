using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class FilesController(
    IFileService fileService,
    IHttpContextAccessor httpContextAccessor
) : BaseController(httpContextAccessor)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Download([FromQuery] string id, CancellationToken cancellationToken)
    {
        var result = await fileService.DownloadFile(id, cancellationToken);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return File(result.Data.Image, "application/octet-stream", id);
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return BadRequest();
        }

        var result = await fileService.UploadFile(file, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}