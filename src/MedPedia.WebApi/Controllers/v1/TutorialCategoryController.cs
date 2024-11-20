using Microsoft.AspNetCore.Mvc;
using MedPedia.Entities.Dto.Tutorial;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class TutorialCategoryController(
    ITutorialCategoryService tutorialCategoryService,
    IHttpContextAccessor httpContextAccessor
) : BaseController(httpContextAccessor)
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllTutorials(CancellationToken cancellationToken)
    {
        var result = await tutorialCategoryService.GetAllTutorials(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddTutorial([FromBody] TutorialCategoryAddUpdateDto tutorialCategoryAddUpdateDto,
        CancellationToken cancellationToken)
    {
        var result = await tutorialCategoryService.AddTutorial(tutorialCategoryAddUpdateDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("[action]/{tutorialId:int}")]
    public async Task<IActionResult> UpdateTutorial(int tutorialId,
        [FromBody] TutorialCategoryAddUpdateDto tutorialCategoryAddUpdateDto, CancellationToken cancellationToken)
    {
        var result = await tutorialCategoryService.UpdateTutorial(tutorialId, tutorialCategoryAddUpdateDto, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("[action]/{tutorialId:int}")]
    public async Task<IActionResult> DeleteTutorial(int tutorialId, CancellationToken cancellationToken)
    {
        var result = await tutorialCategoryService.DeleteTutorial(tutorialId, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
}