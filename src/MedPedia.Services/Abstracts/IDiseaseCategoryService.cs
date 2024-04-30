using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.DiseaseCategory;

namespace MedPedia.Services.Abstracts;

public interface IDiseaseCategoryService
{
    Task<IListResult<List<DiseaseCategoryGetDto>>> GetAllDiseaseCategory(
        CancellationToken cancellationToken);


    Task<IListResult<List<DiseaseCategoryGetDto>>>SearchDiseaseCategory(
        string text,  CancellationToken cancellationToken);
    
    
    Task<IDataResult<DiseaseCategoryGetDto>> AddDiseaseCategory(
        CurrentUserDto currentUser, DiseaseCategoryAddDto diseaseCategoryAddDto, CancellationToken cancellationToken);
}