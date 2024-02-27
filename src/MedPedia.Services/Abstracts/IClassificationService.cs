using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Classification;

namespace MedPedia.Services.Abstracts;

public interface IClassificationService
{
    Task<IListResult<List<ClassificationGetDto>>> GetAllClassification(
        CancellationToken cancellationToken);


    Task<IListResult<List<ClassificationGetDto>>>SearchClassification(
        
       string text, CancellationToken cancellationToken);
    Task<IDataResult<ClassificationGetDto>> AddClassification(
        CurrentUserDto currentUser, ClassificationAddDto classificationAddDto, CancellationToken cancellationToken);

}