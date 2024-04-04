using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Classification;
using MedPedia.Entities.Dto.Prescription;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class PrescriptionService : IPrescriptionService, IClassificationService, IScopedDependency
{
    private readonly IRepository<Prescription> _prescriptionRepository;
    private readonly IRepository<ClassificationOfPrescriptions> _classificationRepository;

    public PrescriptionService(IRepository<Prescription> prescriptionRepository,
        IRepository<ClassificationOfPrescriptions> classificationRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _classificationRepository = classificationRepository;
    }

    public async Task<IListResult<List<PrescriptionGetDto>>> GetAllPrescription(CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionRepository
            .GetListAsync(prescription => prescription.IsDeleted == false, cancellationToken);


        var sortedPrescriptions = prescriptions
            .Select(prescription => new PrescriptionGetDto
            {
                Id = prescription.Id,
                Title = prescription.Title,
                ImageId = prescription.ImageId
            })
            .OrderBy(diseaseDto => diseaseDto.Title, new DrugService.PersianComparer())
            .ToList();

        return new SuccessListResult<List<PrescriptionGetDto>>(sortedPrescriptions);
    }

    public async Task<IListResult<List<PrescriptionGetDto>>> GetAllPrescriptionByClassificationId(int classificationId,
        CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionRepository
            .GetListAsync(
                prescription =>
                    prescription.ClassificationId == classificationId &&
                    prescription.IsDeleted == false,
                cancellationToken);
        var prescriptionsDto = prescriptions.Select(prescription => new PrescriptionGetDto
                { Id = prescription.Id, Title = prescription.Title, ImageId = prescription.ImageId })
            .ToList();
        return new SuccessListResult<List<PrescriptionGetDto>>(prescriptionsDto);
    }

    public async Task<IDataResult<PrescriptionGetDto>> AddPrescription(CurrentUserDto currentUser,
        PrescriptionAddDto prescriptionAddDto,
        CancellationToken cancellationToken)
    {
        var prescription = new Prescription
        {
            Title = prescriptionAddDto.Title,
            ImageId = prescriptionAddDto.ImageId,
            ClassificationId = prescriptionAddDto.ClassificationId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _prescriptionRepository.AddAsync(prescription, cancellationToken);

        var prescriptionDto = prescription.Adapt<PrescriptionGetDto>();
        return new SuccessDataResult<PrescriptionGetDto>(prescriptionDto);
    }

    public async Task<IListResult<List<PrescriptionGetDto>>> SearchPrescription(string text,
        CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionRepository
            .GetListAsync(prescription => prescription.IsDeleted == false && prescription.Title.Contains(text),
                cancellationToken);

        var prescriptionsDto = prescriptions.Select(prescription => new PrescriptionGetDto
                { Id = prescription.Id, Title = prescription.Title, ImageId = prescription.ImageId })
            .ToList();
        return new SuccessListResult<List<PrescriptionGetDto>>(prescriptionsDto);
    }

    public async Task<IListResult<List<ClassificationGetDto>>> GetAllClassification(CancellationToken cancellationToken)
    {
        var classifications = await _prescriptionRepository
            .GetListAsync(prescription => prescription.IsDeleted == false, cancellationToken);

        var classificationsDto = classifications.Select(classification => new ClassificationGetDto
                { Id = classification.Id, Title = classification.Title, ImageId = classification.ImageId })
            .ToList();
        return new SuccessListResult<List<ClassificationGetDto>>(classificationsDto);
    }

    public async Task<IListResult<List<ClassificationGetDto>>> SearchClassification(string text,
        CancellationToken cancellationToken)
    {
        var classifications = await _prescriptionRepository
            .GetListAsync(prescription => prescription.IsDeleted == false && prescription.Title.Contains(text),
                cancellationToken);

        var classificationsDto = classifications.Select(classification => new ClassificationGetDto
                { Id = classification.Id, Title = classification.Title, ImageId = classification.ImageId })
            .ToList();
        return new SuccessListResult<List<ClassificationGetDto>>(classificationsDto);
    }

    public async Task<IDataResult<ClassificationGetDto>> AddClassification(CurrentUserDto currentUser,
        ClassificationAddDto classificationAddDto,
        CancellationToken cancellationToken)
    {
        var classification = new ClassificationOfPrescriptions
        {
            Title = classificationAddDto.Title,
            ImageId = classificationAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _classificationRepository.AddAsync(classification, cancellationToken);

        var classificationDto = classification.Adapt<ClassificationGetDto>();
        return new SuccessDataResult<ClassificationGetDto>(classificationDto);
    }
}