using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.DrugCategory;

namespace MedPedia.Services.Abstracts;

public interface IDrugCategoryService
{
    Task<IListResult<List<DrugCategoryGetDto>>> GetAllDrugCategories(CancellationToken cancellationToken);
    
    Task<IListResult<List<DrugCategoryGetDto>>> SearchDrugCategories( string text, CancellationToken cancellationToken);
    
    Task<IDataResult<DrugCategoryGetDto>> AddDrugCategories(CurrentUserDto currentUser,
        DrugCategoryAddDto drugCategoryAddDto, CancellationToken cancellationToken);
}