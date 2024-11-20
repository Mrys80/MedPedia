using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Tutorial;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class TutorialService(IRepository<Tutorial> tutorialRepository) : ITutorialService, IScopedDependency
{
    public async Task<ListResult<List<TutorialDto>>> GetAllTutorials(CancellationToken cancellationToken)
    {
        var tutorial = await tutorialRepository.GetListAsync(
            t =>
                t.IsDeleted == false, cancellationToken: cancellationToken);

        var tutorialDtos = tutorial.Adapt<List<TutorialDto>>();

        return new SuccessListResult<List<TutorialDto>>(tutorialDtos);
    }

    public async Task<DataResult<TutorialDto>> AddTutorial(TutorialAddUpdateDto tutorialAddUpdateDto,
        CurrentUserDto currentUserDto,
        CancellationToken cancellationToken)
    {
        var tutorial = new Tutorial
        {
            Image = tutorialAddUpdateDto.Image,
            Title = tutorialAddUpdateDto.Title,
            RegDate = DateTime.Now,
            RegUserId = currentUserDto.UserId
        };

        await tutorialRepository.AddAsync(tutorial, cancellationToken);

        var tutorialDto = tutorial.Adapt<TutorialDto>();

        return new SuccessDataResult<TutorialDto>(tutorialDto);
    }

    public async Task<DataResult<TutorialDto>> UpdateTutorial(int tutorialId, TutorialAddUpdateDto tutorialAddUpdateDto,
        CancellationToken cancellationToken)
    {
        var tutorial = await tutorialRepository.GetAsync(
            t => t.Id == tutorialId && t.IsDeleted == false, cancellationToken);

        if (tutorial == null)
        {
            return new ErrorDataResult<TutorialDto>(Messages.NotFound);
        }

        tutorial.Image = tutorialAddUpdateDto.Image;
        tutorial.Title = tutorialAddUpdateDto.Title;

        await tutorialRepository.UpdateAsync(tutorial, cancellationToken);

        var tutorialDto = tutorial.Adapt<TutorialDto>();

        return new SuccessDataResult<TutorialDto>(tutorialDto);
    }

    public async Task<Result> DeleteTutorial(int tutorialId, CurrentUserDto currentUserDto,
        CancellationToken cancellationToken)
    {
        var tutorial = await tutorialRepository.GetAsync(
            t =>
                t.Id == currentUserDto.UserId &&
                t.IsDeleted == false,
            cancellationToken);

        tutorial.IsDeleted = true;

        await tutorialRepository.UpdateAsync(tutorial, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }
}