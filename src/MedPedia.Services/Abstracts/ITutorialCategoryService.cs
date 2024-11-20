using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Tutorial;

namespace MedPedia.Services.Abstracts;

public interface ITutorialCategoryService
{
    Task<ListResult<List<TutorialCategoryDto>>> GetAllTutorials(CancellationToken cancellationToken);

    Task<DataResult<TutorialCategoryDto>> AddTutorial(TutorialCategoryAddUpdateDto tutorialCategoryAddUpdateDto, CurrentUserDto currentUserDto,
        CancellationToken cancellationToken);

    Task<DataResult<TutorialCategoryDto>> UpdateTutorial(int tutorialId, TutorialCategoryAddUpdateDto tutorialCategoryAddUpdateDto,
        CancellationToken cancellationToken);

    Task<Result> DeleteTutorial(int tutorialId, CurrentUserDto currentUserDto, CancellationToken cancellationToken);
}