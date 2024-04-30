using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Disease;

namespace MedPedia.Services.Abstracts;

public interface IDiseaseService
{
    Task<IListResult<List<DiseaseGetDto>>> GetAllDisease(
        CancellationToken cancellationToken
    );

    Task<IListResult<List<DiseaseGetDto>>> GetAllDiseaseByCategoryId(
        int diseaseCategoryId,
        CancellationToken cancellationToken
    );

    Task<IDataResult<DiseaseGetDto>> AddDisease(
        CurrentUserDto currentUser,
        DiseaseAddDto diseaseAddDto,
        CancellationToken cancellationToken
    );

    Task<IListResult<List<DiseaseGetDto>>> SearchDisease(
        string text, CancellationToken cancellationToken
    );


    Task<IDataResult<DiseaseInfo>> GetDiseaseInfo(int diseaseId,
        CancellationToken cancellationToken);
}