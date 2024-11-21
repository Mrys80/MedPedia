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

    public async Task<IDataResult<DiseaseInformationDto>> GetDiseaseDetail(int diseaseId,
        CancellationToken cancellationToken)
    {
        var persianTitles = GetDiseasePersianTitles();

        var diseaseDetailQuery =
            from disease in _diseaseRepository.TableNoTracking.Where(d => d.Id == diseaseId && d.IsDeleted == false)
            join diseaseCategory in _diseaseCategoryRepository.TableNoTracking.Where(dc => dc.IsDeleted == false) on
                disease.DiseaseCategoryId equals diseaseCategory.Id
            join diseaseInformation in _diseaseInformationRepository.TableNoTracking.Where(di => di.IsDeleted == false)
                on disease.Id equals diseaseInformation.DiseaseId
            select new DiseaseInformationDto
            {
                DiseaseId = disease.Id,
                FaTitle = disease.FaTitle,
                EnTitle = disease.EnTitle,
                ImageId = disease.ImageId == null
                    ? null
                    : new KeyValueDto
                    {
                        Key = persianTitles["ImageId"],
                        Value = disease.ImageId
                    },
                DefinitionOfDisease = new KeyValueDto
                {
                    Key = persianTitles["DefinitionOfDisease"],
                    Value = diseaseInformation.DefinitionOfDisease
                },
                Signs = new KeyValueDto
                {
                    Key = persianTitles["Signs"],
                    Value = diseaseInformation.Signs
                },
                Cause = new KeyValueDto
                {
                    Key = persianTitles["Cause"],
                    Value = diseaseInformation.Cause
                },
                RiskFactors = new KeyValueDto
                {
                    Key = persianTitles["RiskFactors"],
                    Value = diseaseInformation.RiskFactors
                },
                TransmissionWays = new KeyValueDto
                {
                    Key = persianTitles["TransmissionWays"],
                    Value = diseaseInformation.TransmissionWays
                },
                Complications = new KeyValueDto
                {
                    Key = persianTitles["Complications"],
                    Value = diseaseInformation.Complications
                },
                PreparingForVisit = new KeyValueDto
                {
                    Key = persianTitles["PreparingForVisit"],
                    Value = diseaseInformation.PreparingForVisit
                },
                WhenVisit = new KeyValueDto
                {
                    Key = persianTitles["WhenVisit"],
                    Value = diseaseInformation.WhenVisit
                },
                Recovery = new KeyValueDto
                {
                    Key = persianTitles["Recovery"],
                    Value = diseaseInformation.Recovery
                },
                DiagnosticTests = new KeyValueDto
                {
                    Key = persianTitles["DiagnosticTests"],
                    Value = diseaseInformation.DiagnosticTests
                },
                DrugTreatment = new KeyValueDto
                {
                    Key = persianTitles["DrugTreatment"],
                    Value = diseaseInformation.DrugTreatment
                },
                Treatment = new KeyValueDto
                {
                    Key = persianTitles["Treatment"],
                    Value = diseaseInformation.Treatment
                },
                ChangeOfLifestyle = new KeyValueDto
                {
                    Key = persianTitles["ChangeOfLifestyle"],
                    Value = diseaseInformation.ChangeOfLifestyle
                },
                ImportantPoints = new KeyValueDto
                {
                    Key = persianTitles["ImportantPoints"],
                    Value = diseaseInformation.ImportantPoints
                },
                BasicPointsOfTreatment = new KeyValueDto
                {
                    Key = persianTitles["BasicPointsOfTreatment"],
                    Value = diseaseInformation.BasicPointsOfTreatment
                },
                Prevention = new KeyValueDto
                {
                    Key = persianTitles["Prevention"],
                    Value = diseaseInformation.Prevention
                }
            };

        var diseaseDetail = await diseaseDetailQuery.FirstOrDefaultAsync(cancellationToken);

        if (diseaseDetail == null)
        {
            return new ErrorDataResult<DiseaseInformationDto>(Messages.NotFound);
        }

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