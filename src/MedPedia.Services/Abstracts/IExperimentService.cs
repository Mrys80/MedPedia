using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Experiment;

namespace MedPedia.Services.Abstracts;

public interface IExperimentService
{
    Task<IListResult<List<ExperimentGetDto>>> GetAllExperiment(
        CancellationToken cancellationToken
    );

    Task<IListResult<List<ExperimentGetDto>>> GetAllExperimentByCategoryId(
        int categoryId,
        CancellationToken cancellationToken
    );

    Task<IDataResult<ExperimentGetDto>> AddExperiment(
        CurrentUserDto currentUser,
        ExperimentAddDto experimentAddDto,
        CancellationToken cancellationToken
    );


    Task<IListResult<List<ExperimentCategoryGetDto>>> GetAllExperimentCategory(
        CancellationToken cancellationToken);


    Task<IDataResult<ExperimentCategoryGetDto>> AddExperimentCategory(
        CurrentUserDto currentUser, ExperimentCategoryAddDto experimentCategoryAddDto,
        CancellationToken cancellationToken);
    
    
    Task<IDataResult<ExperimentDetailDto>> GetExperimentInfo(int experimentId,
        CancellationToken cancellationToken);

    Task<IDataResult<ExperimentInformationDto>> GetExperimentDetail(int experimentId,
        CancellationToken cancellationToken);
}