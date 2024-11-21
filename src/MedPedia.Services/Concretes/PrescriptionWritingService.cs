using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.PrescriptionWriting;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class PrescriptionWritingService(IRepository<PrescriptionWriting> prescriptionWritingRepository)
    : IPrescriptionWritingService, IScopedDependency
{
    public async Task<IListResult<List<PrescriptionWritingGetDto>>> GetAllPrescriptions(
        CancellationToken cancellationToken)
    {
        var prescriptionWriting = await prescriptionWritingRepository
            .GetListAsync(prescriptionWriting => prescriptionWriting.IsDeleted == false, cancellationToken);

        var prescriptionWritingDto = prescriptionWriting.Select(prescription => new PrescriptionWritingGetDto
            {
                Id = prescription.Id,
                FaTitle = prescription.FaTitle,
                ImageId = prescription.ImageId,
                Description = prescription.Description,
                EnTitle = prescription.EnTitle,
            })
            .ToList();
        return new SuccessListResult<List<PrescriptionWritingGetDto>>(prescriptionWritingDto);
    }

    public async Task<IDataResult<PrescriptionWritingDetailDto>> GetPrescriptionDetail(int prescriptionId,
        CancellationToken cancellationToken)
    {
        var prescription = await prescriptionWritingRepository
            .GetAsync(
                p => p.Id == prescriptionId && !p.IsDeleted,
                cancellationToken);

        if (prescription == null)
        {
            return new ErrorDataResult<PrescriptionWritingDetailDto>(Messages.NotFound);
        }

        var prescriptionDetailDto = new PrescriptionWritingDetailDto
        {
            Id = prescription.Id,
            FaTitle = prescription.FaTitle,
            ImageId = prescription.ImageId,
            EnTitle = prescription.EnTitle,
            Description = prescription.Description,
            DrugIds = prescription.DrugIds.Split(',').Select(int.Parse).ToList(),
            DiseaseIds = prescription.DiseaseId
        };

        return new SuccessDataResult<PrescriptionWritingDetailDto>(prescriptionDetailDto);
    }


    public async Task<IDataResult<PrescriptionWritingGetDto>> AddPrescription(CurrentUserDto currentUser,
        PrescriptionWritingAddDto prescriptionDto,
        CancellationToken cancellationToken)
    {
        var prescriptionWriting = new PrescriptionWriting
        {
            Description = prescriptionDto.Description,
            ImageId = prescriptionDto.ImageId,
            FaTitle = prescriptionDto.FaTitle,
            EnTitle = prescriptionDto.EnTitle,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await prescriptionWritingRepository.AddAsync(prescriptionWriting, cancellationToken);

        var prescriptionWritingDto = prescriptionWriting.Adapt<PrescriptionWritingGetDto>();

        return new SuccessDataResult<PrescriptionWritingGetDto>(prescriptionWritingDto);
    }

    public async Task<IListResult<List<PrescriptionWritingGetDto>>> GetPrescriptionsByDiseaseId(int diseaseId,
        CancellationToken cancellationToken)
    {
        var prescriptions = await prescriptionWritingRepository
            .TableNoTracking
            .Where(prescription => prescription.DiseaseId == diseaseId && !prescription.IsDeleted)
            .ToListAsync(cancellationToken);

        var prescriptionsDto = prescriptions.Select(prescription => new PrescriptionWritingGetDto
            {
                Id = prescription.Id,
                FaTitle = prescription.FaTitle,
                ImageId = prescription.ImageId,
                EnTitle = prescription.EnTitle,
                Description = prescription.Description,
            })
            .ToList();

        return new SuccessListResult<List<PrescriptionWritingGetDto>>(prescriptionsDto);
    }

    public async Task<IListResult<List<PrescriptionWritingGetDto>>> SearchPrescriptions(string text,
        CancellationToken cancellationToken)
    {
        var prescriptions = await prescriptionWritingRepository
            .GetListAsync(prescription => !prescription.IsDeleted && prescription.FaTitle.Contains(text),
                cancellationToken);

        var prescriptionDtos = prescriptions.Select(prescription => new PrescriptionWritingGetDto
        {
            Id = prescription.Id,
            FaTitle = prescription.FaTitle,
            EnTitle = prescription.EnTitle,
            ImageId = prescription.ImageId,
            Description = prescription.Description,
            DrugIds = prescription.DrugIds
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList(),
            DiseaseIds = prescription.DiseaseId
        }).ToList();

        return new SuccessListResult<List<PrescriptionWritingGetDto>>(prescriptionDtos);
    }
}