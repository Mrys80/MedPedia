using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Disease;
using MedPedia.Entities.Dto.DiseaseCategory;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class DiseaseService : IDiseaseService, IDiseaseCategoryService, IScopedDependency
{
    private readonly IRepository<Disease> _diseaseRepository;
    private readonly IRepository<DiseaseCategory> _diseaseCategoryRepository;


    public DiseaseService(IRepository<Disease> diseaseRepository,
        IRepository<DiseaseCategory> diseaseCategoryRepository)
    {
        _diseaseRepository = diseaseRepository;
        _diseaseCategoryRepository = diseaseCategoryRepository;
    }

    public async Task<IListResult<List<DiseaseGetDto>>> GetAllDisease(CancellationToken cancellationToken)
    {
        var diseases = await _diseaseRepository
            .GetListAsync(disease => disease.IsDeleted == false, cancellationToken);

        var sortedDiseases = diseases
            .Select(disease => new DiseaseGetDto
            {
                Id = disease.Id,
                FaTitle = disease.FaTitle,
                EnTitle = disease.EnTitle,
                ImageId = disease.ImageId,
                DefinitionOfDisease = disease.DefinitionOfDisease,
                Signs = disease.Signs,
                Cause = disease.Cause,
                RiskFactors = disease.RiskFactors,
                TransmissionWays = disease.TransmissionWays,
                Complications = disease.Complications,
                PreparingForVisit = disease.PreparingForVisit,
                WhenVisit = disease.WhenVisit,
                Recovery = disease.Recovery,
                DiagnosticTests = disease.DiagnosticTests,
                DrugTreatment = disease.DrugTreatment,
                Treatment = disease.Treatment,
                ChangeOfLifestyle = disease.ChangeOfLifestyle,
                ImportantPoints = disease.ImportantPoints,
                BasicPointsOfTreatment = disease.BasicPointsOfTreatment,
                Prevention = disease.Prevention
            })
            .OrderBy(diseaseDto => diseaseDto.FaTitle, new DrugService.PersianComparer())
            .ToList();


        return new SuccessListResult<List<DiseaseGetDto>>(sortedDiseases);
    }

    public async Task<IListResult<List<DiseaseGetDto>>> GetAllDiseaseByCategoryId(int diseaseCategoryId,
        CancellationToken cancellationToken)
    {
        var diseases = await _diseaseRepository
            .GetListAsync(
                disease =>
                    disease.DiseaseCategoryId == diseaseCategoryId &&
                    disease.IsDeleted == false,
                cancellationToken);
        var diseasesDto = diseases.Select(disease => new DiseaseGetDto
            {
                Id = disease.Id,
                FaTitle = disease.FaTitle,
                EnTitle = disease.EnTitle,
                ImageId = disease.ImageId,
                DefinitionOfDisease = disease.DefinitionOfDisease,
                Signs = disease.Signs,
                Cause = disease.Cause,
                RiskFactors = disease.RiskFactors,
                TransmissionWays = disease.TransmissionWays,
                Complications = disease.Complications,
                PreparingForVisit = disease.PreparingForVisit,
                WhenVisit = disease.WhenVisit,
                Recovery = disease.Recovery,
                DiagnosticTests = disease.DiagnosticTests,
                DrugTreatment = disease.DrugTreatment,
                Treatment = disease.Treatment,
                ChangeOfLifestyle = disease.ChangeOfLifestyle,
                ImportantPoints = disease.ImportantPoints,
                BasicPointsOfTreatment = disease.BasicPointsOfTreatment,
                Prevention = disease.Prevention
            })
            .ToList();
        return new SuccessListResult<List<DiseaseGetDto>>(diseasesDto);
    }

    public async Task<IDataResult<DiseaseGetDto>> AddDisease(CurrentUserDto currentUser, DiseaseAddDto diseaseAddDto,
        CancellationToken cancellationToken)
    {
        var disease = new Disease
        {
            FaTitle = diseaseAddDto.FaTitle,
            EnTitle = diseaseAddDto.EnTitle,
            ImageId = diseaseAddDto.ImageId,
            DefinitionOfDisease = diseaseAddDto.DefinitionOfDisease,
            Signs = diseaseAddDto.Signs,
            Cause = diseaseAddDto.Cause,
            RiskFactors = diseaseAddDto.RiskFactors,
            TransmissionWays = diseaseAddDto.TransmissionWays,
            Complications = diseaseAddDto.Complications,
            PreparingForVisit = diseaseAddDto.PreparingForVisit,
            WhenVisit = diseaseAddDto.WhenVisit,
            Recovery = diseaseAddDto.Recovery,
            DiagnosticTests = diseaseAddDto.DiagnosticTests,
            DrugTreatment = diseaseAddDto.DrugTreatment,
            Treatment = diseaseAddDto.Treatment,
            ChangeOfLifestyle = diseaseAddDto.ChangeOfLifestyle,
            ImportantPoints = diseaseAddDto.ImportantPoints,
            BasicPointsOfTreatment = diseaseAddDto.BasicPointsOfTreatment,
            Prevention = diseaseAddDto.Prevention,
            DiseaseCategoryId = diseaseAddDto.DiseaseCategoryId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _diseaseRepository.AddAsync(disease, cancellationToken);

        var diseaseDto = disease.Adapt<DiseaseGetDto>();
        return new SuccessDataResult<DiseaseGetDto>(diseaseDto);
    }

    public async Task<IListResult<List<DiseaseGetDto>>> SearchDisease(string text, CancellationToken cancellationToken)
    {
        var diseases = await _diseaseRepository
            .GetListAsync(disease => disease.IsDeleted == false && disease.FaTitle.Contains(text), cancellationToken);
        var diseasesDto = diseases.Select(disease => new DiseaseGetDto
            {
                Id = disease.Id,
                FaTitle = disease.FaTitle,
                EnTitle = disease.EnTitle,
                ImageId = disease.ImageId,
                DefinitionOfDisease = disease.DefinitionOfDisease,
                Signs = disease.Signs,
                Cause = disease.Cause,
                RiskFactors = disease.RiskFactors,
                TransmissionWays = disease.TransmissionWays,
                Complications = disease.Complications,
                PreparingForVisit = disease.PreparingForVisit,
                WhenVisit = disease.WhenVisit,
                Recovery = disease.Recovery,
                DiagnosticTests = disease.DiagnosticTests,
                DrugTreatment = disease.DrugTreatment,
                Treatment = disease.Treatment,
                ChangeOfLifestyle = disease.ChangeOfLifestyle,
                ImportantPoints = disease.ImportantPoints,
                BasicPointsOfTreatment = disease.BasicPointsOfTreatment,
                Prevention = disease.Prevention
            })
            .ToList();
        return new SuccessListResult<List<DiseaseGetDto>>(diseasesDto);
    }


    public async Task<IListResult<List<DiseaseCategoryGetDto>>> GetAllDiseaseCategory(
        CancellationToken cancellationToken)
    {
        var diseasesCategory = await _diseaseCategoryRepository
            .GetListAsync(diseasesCategory => diseasesCategory.IsDeleted == false, cancellationToken);
        var diseasesCategoryDto = diseasesCategory.Select(disease => new DiseaseCategoryGetDto
                { Id = disease.Id, Title = disease.Title, ImageId = disease.ImageId })
            .ToList();
        return new SuccessListResult<List<DiseaseCategoryGetDto>>(diseasesCategoryDto);
    }

    public async Task<IListResult<List<DiseaseCategoryGetDto>>> SearchDiseaseCategory(string text,
        CancellationToken cancellationToken)
    {
        var diseasesCategory = await _diseaseCategoryRepository
            .GetListAsync(
                diseasesCategory => diseasesCategory.IsDeleted == false && diseasesCategory.Title.Contains(text),
                cancellationToken);
        var diseasesCategoryDto = diseasesCategory.Select(disease => new DiseaseCategoryGetDto
                { Id = disease.Id, Title = disease.Title, ImageId = disease.ImageId })
            .ToList();
        return new SuccessListResult<List<DiseaseCategoryGetDto>>(diseasesCategoryDto);
    }

    public async Task<IDataResult<DiseaseCategoryGetDto>> AddDiseaseCategory(CurrentUserDto currentUser,
        DiseaseCategoryAddDto diseaseCategoryAddDto, CancellationToken cancellationToken)
    {
        var diseaseCategory = new DiseaseCategory
        {
            Title = diseaseCategoryAddDto.Title,
            ImageId = diseaseCategoryAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _diseaseCategoryRepository.AddAsync(diseaseCategory, cancellationToken);

        var diseaseCategoryDto = diseaseCategory.Adapt<DiseaseCategoryGetDto>();
        return new SuccessDataResult<DiseaseCategoryGetDto>(diseaseCategoryDto);
    }
}