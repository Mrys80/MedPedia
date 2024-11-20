using Microsoft.AspNetCore.Mvc;
using MedPedia.Entities.Dto.Tutorial;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class TutorialController(
    ITutorialService tutorialService,
    IHttpContextAccessor httpContextAccessor
) : BaseController(httpContextAccessor)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllTutorials(CancellationToken cancellationToken)
    {
        var result = await tutorialService.GetAllTutorials(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddTutorial([FromBody] TutorialAddUpdateDto tutorialAddUpdateDto,
        CancellationToken cancellationToken)
    {
        var result = await tutorialService.AddTutorial(tutorialAddUpdateDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("[action]/{tutorialId:int}")]
    public async Task<IActionResult> UpdateTutorial(int tutorialId,
        [FromBody] TutorialAddUpdateDto tutorialAddUpdateDto, CancellationToken cancellationToken)
    {
        var result = await tutorialService.UpdateTutorial(tutorialId, tutorialAddUpdateDto, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("[action]/{tutorialId:int}")]
    public async Task<IActionResult> DeleteTutorial(int tutorialId, CancellationToken cancellationToken)
    {
        var result = await tutorialService.DeleteTutorial(tutorialId, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}