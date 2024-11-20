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

public class TutorialCategoryService(IRepository<TutorialCategory> tutorialRepository) : ITutorialCategoryService, IScopedDependency
{
    public async Task<ListResult<List<TutorialCategoryDto>>> GetAllTutorials(CancellationToken cancellationToken)
    {
        var tutorial = await tutorialRepository.GetListAsync(
            t =>
                t.IsDeleted == false, cancellationToken: cancellationToken);

        var tutorialDtos = tutorial.Adapt<List<TutorialCategoryDto>>();

        return new SuccessListResult<List<TutorialCategoryDto>>(tutorialDtos);
    }

    public async Task<DataResult<TutorialCategoryDto>> AddTutorial(TutorialCategoryAddUpdateDto tutorialCategoryAddUpdateDto,
        CurrentUserDto currentUserDto,
        CancellationToken cancellationToken)
    {
        var tutorial = new TutorialCategory
        {
            Image = tutorialCategoryAddUpdateDto.Image,
            Title = tutorialCategoryAddUpdateDto.Title,
            RegDate = DateTime.Now,
            RegUserId = currentUserDto.UserId
        };

        await tutorialRepository.AddAsync(tutorial, cancellationToken);

        var tutorialDto = tutorial.Adapt<TutorialCategoryDto>();

        return new SuccessDataResult<TutorialCategoryDto>(tutorialDto);
    }

    public async Task<DataResult<TutorialCategoryDto>> UpdateTutorial(int tutorialId, TutorialCategoryAddUpdateDto tutorialCategoryAddUpdateDto,
        CancellationToken cancellationToken)
    {
        var tutorial = await tutorialRepository.GetAsync(
            t => t.Id == tutorialId && t.IsDeleted == false, cancellationToken);

        if (tutorial == null)
        {
            return new ErrorDataResult<TutorialCategoryDto>(Messages.NotFound);
        }

        tutorial.Image = tutorialCategoryAddUpdateDto.Image;
        tutorial.Title = tutorialCategoryAddUpdateDto.Title;

        await tutorialRepository.UpdateAsync(tutorial, cancellationToken);

        var tutorialDto = tutorial.Adapt<TutorialCategoryDto>();

        return new SuccessDataResult<TutorialCategoryDto>(tutorialDto);
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