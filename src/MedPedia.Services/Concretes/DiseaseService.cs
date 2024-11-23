using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Disease;
using MedPedia.Entities.Dto.DiseaseCategory;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.Experiment;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class DiseaseService : IDiseaseService, IDiseaseCategoryService, IScopedDependency
{
    private readonly IRepository<Disease> _diseaseRepository;
    private readonly IRepository<DiseaseCategory> _diseaseCategoryRepository;
    private readonly IRepository<DiseaseInformation> _diseaseInformationRepository;


    public DiseaseService(IRepository<Disease> diseaseRepository,
        IRepository<DiseaseCategory> diseaseCategoryRepository,
        IRepository<DiseaseInformation> diseaseInformationRepository)
    {
        _diseaseRepository = diseaseRepository;
        _diseaseCategoryRepository = diseaseCategoryRepository;
        _diseaseInformationRepository = diseaseInformationRepository;
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
            })
            .ToList();
        return new SuccessListResult<List<DiseaseGetDto>>(diseasesDto);
    }

    public async Task<IDataResult<DiseaseInfo>> GetDiseaseInfo(int diseaseId, CancellationToken cancellationToken)
    {
        var diseaseInfo = await _diseaseInformationRepository
            .GetAsync(
                x => x.DiseaseId == diseaseId,
                cancellationToken
            );

        var diseaseInfoDto = diseaseInfo.Adapt<DiseaseInfo>();
        return new SuccessDataResult<DiseaseInfo>(diseaseInfoDto);
    }

    public async Task<IDataResult<DiseaseInformationDto>> GetDiseaseDetail(
        int diseaseId,
        CancellationToken cancellationToken)
    {
        var persianTitles = GetDiseasePersianTitles();

        var rawQuery =
            from disease in _diseaseRepository.TableNoTracking.Where(d => d.Id == diseaseId && !d.IsDeleted)
            join diseaseCategory in _diseaseCategoryRepository.TableNoTracking.Where(dc => !dc.IsDeleted)
                on disease.DiseaseCategoryId equals diseaseCategory.Id
            join diseaseInformation in _diseaseInformationRepository.TableNoTracking.Where(di => !di.IsDeleted)
                on disease.Id equals diseaseInformation.DiseaseId
            select new
            {
                Disease = disease,
                DiseaseCategory = diseaseCategory,
                DiseaseInformation = diseaseInformation
            };

        var rawData = await rawQuery.FirstOrDefaultAsync(cancellationToken);

        if (rawData == null)
        {
            return new ErrorDataResult<DiseaseInformationDto>(Messages.NotFound);
        }

        var diseaseDetail = new DiseaseInformationDto
        {
            DiseaseId = rawData.Disease.Id,
            FaTitle = rawData.Disease.FaTitle,
            EnTitle = rawData.Disease.EnTitle,
            ImageId = rawData.Disease.ImageId == null
                ? null
                : new KeyValueDto
                {
                    Key = persianTitles["ImageId"],
                    Value = rawData.Disease.ImageId
                },
            DefinitionOfDisease = new KeyValueDto
            {
                Key = persianTitles["DefinitionOfDisease"],
                Value = rawData.DiseaseInformation.DefinitionOfDisease
            },
            Signs = new KeyValueDto
            {
                Key = persianTitles["Signs"],
                Value = rawData.DiseaseInformation.Signs
            },
            Cause = new KeyValueDto
            {
                Key = persianTitles["Cause"],
                Value = rawData.DiseaseInformation.Cause
            },
            RiskFactors = new KeyValueDto
            {
                Key = persianTitles["RiskFactors"],
                Value = rawData.DiseaseInformation.RiskFactors
            },
            TransmissionWays = new KeyValueDto
            {
                Key = persianTitles["TransmissionWays"],
                Value = rawData.DiseaseInformation.TransmissionWays
            },
            Complications = new KeyValueDto
            {
                Key = persianTitles["Complications"],
                Value = rawData.DiseaseInformation.Complications
            },
            PreparingForVisit = new KeyValueDto
            {
                Key = persianTitles["PreparingForVisit"],
                Value = rawData.DiseaseInformation.PreparingForVisit
            },
            WhenVisit = new KeyValueDto
            {
                Key = persianTitles["WhenVisit"],
                Value = rawData.DiseaseInformation.WhenVisit
            },
            Recovery = new KeyValueDto
            {
                Key = persianTitles["Recovery"],
                Value = rawData.DiseaseInformation.Recovery
            },
            DiagnosticTests = new KeyValueDto
            {
                Key = persianTitles["DiagnosticTests"],
                Value = rawData.DiseaseInformation.DiagnosticTests
            },
            DrugTreatment = new KeyValueDto
            {
                Key = persianTitles["DrugTreatment"],
                Value = rawData.DiseaseInformation.DrugTreatment
            },
            Treatment = new KeyValueDto
            {
                Key = persianTitles["Treatment"],
                Value = rawData.DiseaseInformation.Treatment
            },
            ChangeOfLifestyle = new KeyValueDto
            {
                Key = persianTitles["ChangeOfLifestyle"],
                Value = rawData.DiseaseInformation.ChangeOfLifestyle
            },
            ImportantPoints = new KeyValueDto
            {
                Key = persianTitles["ImportantPoints"],
                Value = rawData.DiseaseInformation.ImportantPoints
            },
            BasicPointsOfTreatment = new KeyValueDto
            {
                Key = persianTitles["BasicPointsOfTreatment"],
                Value = rawData.DiseaseInformation.BasicPointsOfTreatment
            },
            Prevention = new KeyValueDto
            {
                Key = persianTitles["Prevention"],
                Value = rawData.DiseaseInformation.Prevention
            }
        };

        return new SuccessDataResult<DiseaseInformationDto>(diseaseDetail);
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


    private static Dictionary<string, string> GetDiseasePersianTitles()
    {
        return new Dictionary<string, string>
        {
            { "DefinitionOfDisease", "تعریف بیماری" },
            { "Signs", "علائم" },
            { "Cause", "علت" },
            { "RiskFactors", "عوامل خطر" },
            { "TransmissionWays", "راه‌های انتقال" },
            { "Complications", "عوارض" },
            { "PreparingForVisit", "آمادگی برای مراجعه" },
            { "WhenVisit", "زمان مراجعه" },
            { "Recovery", "بهبودی" },
            { "DiagnosticTests", "آزمایش‌های تشخیصی" },
            { "DrugTreatment", "درمان دارویی" },
            { "Treatment", "درمان" },
            { "ChangeOfLifestyle", "تغییر سبک زندگی" },
            { "ImportantPoints", "نکات مهم" },
            { "BasicPointsOfTreatment", "نکات پایه درمان" },
            { "Prevention", "پیشگیری" }
        };
    }
}