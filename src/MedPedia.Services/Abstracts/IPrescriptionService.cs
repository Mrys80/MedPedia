using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.Prescription;

namespace MedPedia.Services.Abstracts;

public interface IPrescriptionService
{
    Task<IListResult<List<PrescriptionGetDto>>> GetAllPrescription(CancellationToken cancellationToken);
    Task<IListResult<List<PrescriptionGetDto>>> GetAllPrescriptionByClassificationId(int classificationId, CancellationToken cancellationToken);

    Task<IDataResult<PrescriptionGetDto>> AddPrescription(CurrentUserDto currentUser, PrescriptionAddDto prescriptionAddDto,
        CancellationToken cancellationToken);
    
    Task<IListResult<List<PrescriptionGetDto>>> SearchPrescription( string text, CancellationToken cancellationToken);

}