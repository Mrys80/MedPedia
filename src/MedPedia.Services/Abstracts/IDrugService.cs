using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Drug;

namespace MedPedia.Services.Abstracts;

public interface IDrugService
{
    Task<IListResult<List<DrugGetDto>>> GetAllDrug(CancellationToken cancellationToken);
    Task<IListResult<List<DrugGetDto>>> GetAllDrugByCategoryId(int drugCategoryId, CancellationToken cancellationToken);

    Task<IDataResult<DrugGetDto>> AddDrug(CurrentUserDto currentUser, DrugAddDto drugAddDto,
        CancellationToken cancellationToken);
    
    Task<IListResult<List<DrugGetDto>>> SearchDrug( string text, CancellationToken cancellationToken);

}