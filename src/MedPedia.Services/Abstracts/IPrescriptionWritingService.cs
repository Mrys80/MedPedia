using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.PrescriptionWriting;

namespace MedPedia.Services.Abstracts;

public interface IPrescriptionWritingService
{
    Task<IListResult<List<PrescriptionWritingGetDto>>> GetAllPrescriptions(CancellationToken cancellationToken);

    Task<IDataResult<PrescriptionWritingDetailDto>> GetPrescriptionDetail(int prescriptionId,
        CancellationToken cancellationToken);

    Task<IListResult<List<PrescriptionWritingCategoryGetDto>>> GetAllCategories(CancellationToken cancellationToken);

    Task<IDataResult<PrescriptionWritingCategoryGetDto>> AddCategory(CurrentUserDto currentUser,
        PrescriptionWritingCategoryAddDto addDto, CancellationToken cancellationToken);

    Task<IDataResult<PrescriptionWritingGetDto>> AddPrescription(CurrentUserDto currentUser,
        PrescriptionWritingAddDto prescriptionDto, CancellationToken cancellationToken);

    Task<IListResult<List<PrescriptionWritingGetDto>>> GetPrescriptionsByDiseaseId(int diseaseId,
        CancellationToken cancellationToken);

    Task<IListResult<List<PrescriptionWritingGetDto>>> SearchPrescriptions(string text,
        CancellationToken cancellationToken);
}