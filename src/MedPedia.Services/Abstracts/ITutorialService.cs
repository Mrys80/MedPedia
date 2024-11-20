using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Tutorial;

namespace MedPedia.Services.Abstracts;

public interface ITutorialService
{
    Task<ListResult<List<TutorialDto>>> GetAllTutorials(CancellationToken cancellationToken);

    Task<DataResult<TutorialDto>> AddTutorial(TutorialAddUpdateDto tutorialAddUpdateDto, CurrentUserDto currentUserDto,
        CancellationToken cancellationToken);

    Task<DataResult<TutorialDto>> UpdateTutorial(int tutorialId, TutorialAddUpdateDto tutorialAddUpdateDto,
        CancellationToken cancellationToken);

    Task<Result> DeleteTutorial(int tutorialId, CurrentUserDto currentUserDto, CancellationToken cancellationToken);
}