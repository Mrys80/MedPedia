using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.ClassificationOfPregnancy;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.Indications;
using MedPedia.Entities.Dto.Martindale;
using MedPedia.Entities.Dto.MethodOfUse;
using MedPedia.Entities.Dto.PharmaceuticalForms;
using MedPedia.Entities.Dto.SideEffect;
using MedPedia.Entities.Dto.TherapeuticClassification;

namespace MedPedia.Services.Abstracts;

public interface IDrugService
{
    Task<IListResult<List<DrugGetDto>>> GetAllDrug(CancellationToken cancellationToken);
    Task<IListResult<List<DrugGetDto>>> GetAllDrugByCategoryId(int drugCategoryId, CancellationToken cancellationToken);

    Task<IDataResult<DrugGetDto>> AddDrug(CurrentUserDto currentUser, DrugAddDto drugAddDto,
        CancellationToken cancellationToken);

    Task<IListResult<List<DrugGetDto>>> SearchDrug(string text, CancellationToken cancellationToken);

    Task<IListResult<List<ClassificationOfPregnancyGetDto>>> GetAllPregnancyClassification(
        CancellationToken cancellationToken);

    Task<IListResult<List<MartindaleGetDto>>> GetAllMartindale(CancellationToken cancellationToken);
    Task<IListResult<List<MethodOfUseGetDto>>> GetAllMethodOfUse(CancellationToken cancellationToken);
    Task<IListResult<List<PharmaceuticalFormsGetDto>>> GetAllPharmaceuticalForms(CancellationToken cancellationToken);

    Task<IListResult<List<PharmaceuticalFormsGetDto>>> GetAllPharmacologicalClassification(
        CancellationToken cancellationToken);


    Task<IListResult<List<TherapeuticClassificationGetDto>>> GetAllTherapeuticClassification(
        CancellationToken cancellationToken);

    Task<IDataResult<DrugDetailDto>> GetDrugDetail(int drugId,
        CancellationToken cancellationToken);
}