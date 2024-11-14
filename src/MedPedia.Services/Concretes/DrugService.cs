using System.Globalization;
using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.ClassificationOfPregnancy;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.DrugCategory;
using MedPedia.Entities.Dto.Indications;
using MedPedia.Entities.Dto.Martindale;
using MedPedia.Entities.Dto.MethodOfUse;
using MedPedia.Entities.Dto.PharmaceuticalForms;
using MedPedia.Entities.Dto.SideEffect;
using MedPedia.Entities.Dto.TherapeuticClassification;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class DrugService : IDrugService, IDrugCategoryService, IScopedDependency
{
    private readonly IRepository<Drug> _drugRepository;
    private readonly IRepository<DrugCategory> _drugCategoryRepository;
    private readonly IRepository<ClassificationOfPregnancy> _classificationOfPregnancyRepository;
    private readonly IRepository<MartindaleClassification> _martindaleRepository;
    private readonly IRepository<MethodOfUse> _methodOfUseRepository;
    private readonly IRepository<PharmaceuticalForms> _pharmaceuticalFormsRepository;
    private readonly IRepository<PharmacologicalClassification> _pharmacologicalClassificationRepository;
    private readonly IRepository<TherapeuticClassification> _therapeuticClassificationRepository;
    private readonly IRepository<DrugInformation> _drugInformationRepository;
    private readonly IRepository<DrugForms> _drugFormRepository;


    public DrugService(IRepository<Drug> drugRepository, IRepository<DrugCategory> drugCategoryRepository,
        IRepository<ClassificationOfPregnancy> classificationOfPregnancyRepository,
        IRepository<MartindaleClassification> martindaleRepository, IRepository<MethodOfUse> methodOfUseRepository,
        IRepository<PharmaceuticalForms> pharmaceuticalFormsRepository,
        IRepository<PharmacologicalClassification> pharmacologicalClassificationRepository,
        IRepository<TherapeuticClassification> therapeuticClassificationRepository,
        IRepository<DrugInformation> drugInformationRepository, IRepository<DrugForms> drugFormRepository
    )
    {
        _drugRepository = drugRepository;
        _drugCategoryRepository = drugCategoryRepository;
        _classificationOfPregnancyRepository = classificationOfPregnancyRepository;
        _martindaleRepository = martindaleRepository;
        _methodOfUseRepository = methodOfUseRepository;
        _pharmaceuticalFormsRepository = pharmaceuticalFormsRepository;
        _pharmacologicalClassificationRepository = pharmacologicalClassificationRepository;
        _therapeuticClassificationRepository = therapeuticClassificationRepository;
        _drugInformationRepository = drugInformationRepository;
        _drugFormRepository = drugFormRepository;
    }

    public async Task<IListResult<List<DrugGetDto>>> GetAllDrug(
        CancellationToken cancellationToken)
    {
        var drugs = await _drugRepository
            .GetListAsync(drug => drug.IsDeleted == false, cancellationToken);

        var sortedDrugs = drugs
            .Select(drug => new DrugGetDto
            {
                Id = drug.Id,
                FaTitle = drug.FaTitle,
                EnTitle = drug.EnTitle,
                Brand = drug.Brand,
                ImageId = drug.ImageId
            })
            .OrderBy(drugDto => drugDto.FaTitle, new PersianComparer())
            .ToList();


        return new SuccessListResult<List<DrugGetDto>>(sortedDrugs);
    }

    public async Task<IListResult<List<DrugGetDto>>> GetAllDrugByCategoryId(int drugCategoryId,
        CancellationToken cancellationToken)
    {
        var drugs = await _drugRepository
            .GetListAsync(
                drug =>
                    drug.IsDeleted == false &&
                    drug.DrugCategoryId == drugCategoryId
                , cancellationToken);
        var drugsDto = drugs.Select(drug => new DrugGetDto
            {
                Id = drug.Id,
                FaTitle = drug.FaTitle,
                EnTitle = drug.EnTitle,
                Brand = drug.Brand,
                ImageId = drug.ImageId
            })
            .ToList();
        return new SuccessListResult<List<DrugGetDto>>(drugsDto);
    }

    public async Task<IDataResult<DrugGetDto>> AddDrug(CurrentUserDto currentUser, DrugAddDto drugAddDto,
        CancellationToken cancellationToken)
    {
        var drug = new Drug
        {
            FaTitle = drugAddDto.FaTitle,
            EnTitle = drugAddDto.EnTitle,
            Brand = drugAddDto.Brand,
            DrugCategoryId = drugAddDto.DrugCategoryId,
            ImageId = drugAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _drugRepository.AddAsync(drug, cancellationToken);

        var drugDto = drug.Adapt<DrugGetDto>();
        return new SuccessDataResult<DrugGetDto>(drugDto);
    }

    public async Task<IListResult<List<DrugGetDto>>> SearchDrug(string text, CancellationToken cancellationToken)
    {
        var drugs = await _drugRepository
            .GetListAsync(
                drug => drug.IsDeleted == false && (drug.FaTitle.Contains(text) || drug.EnTitle.Contains(text)),
                cancellationToken);
        var drugsDto = drugs.Select(drug => new DrugGetDto
            {
                Id = drug.Id,
                FaTitle = drug.FaTitle,
                EnTitle = drug.EnTitle,
                Brand = drug.Brand,
                ImageId = drug.ImageId
            })
            .ToList();
        return new SuccessListResult<List<DrugGetDto>>(drugsDto);
    }


    public async Task<IListResult<List<DrugCategoryGetDto>>> GetAllDrugCategories(CancellationToken cancellationToken)
    {
        var drugsCategory = await _drugCategoryRepository
            .GetListAsync(drugCategory => drugCategory.IsDeleted == false, cancellationToken);
        var drugsCategoryDto = drugsCategory.Select(drugsCategory => new DrugCategoryGetDto
            {
                Id = drugsCategory.Id,
                FaTitle = drugsCategory.FaTitle,
                EnTitle = drugsCategory.EnTitle,
                ImageId = drugsCategory.ImageId,
            })
            .ToList();
        return new SuccessListResult<List<DrugCategoryGetDto>>(drugsCategoryDto);
    }

    public async Task<IListResult<List<DrugCategoryGetDto>>> SearchDrugCategories(string text,
        CancellationToken cancellationToken)
    {
        var drugsCategory = await _drugCategoryRepository
            .GetListAsync(
                drugCategory => drugCategory.IsDeleted == false &&
                                (drugCategory.FaTitle.Contains(text) || drugCategory.EnTitle.Contains(text)),
                cancellationToken);
        var drugsCategoryDto = drugsCategory.Select(drugsCategory => new DrugCategoryGetDto
            {
                Id = drugsCategory.Id,
                FaTitle = drugsCategory.FaTitle,
                EnTitle = drugsCategory.EnTitle,
                ImageId = drugsCategory.ImageId,
            })
            .ToList();
        return new SuccessListResult<List<DrugCategoryGetDto>>(drugsCategoryDto);
    }

    public async Task<IDataResult<DrugCategoryGetDto>> AddDrugCategories(
        CurrentUserDto currentUser,
        DrugCategoryAddDto drugCategoryAddDto, CancellationToken cancellationToken)
    {
        var drugCategory = new DrugCategory
        {
            FaTitle = drugCategoryAddDto.FaTitle,
            EnTitle = drugCategoryAddDto.EnTitle,
            ImageId = drugCategoryAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _drugCategoryRepository.AddAsync(drugCategory, cancellationToken);

        var drugCategoryDto = drugCategory.Adapt<DrugCategoryGetDto>();
        return new SuccessDataResult<DrugCategoryGetDto>(drugCategoryDto);
    }


    public async Task<IListResult<List<ClassificationOfPregnancyGetDto>>> GetAllPregnancyClassification(
        CancellationToken cancellationToken)
    {
        var pregnancies = await _classificationOfPregnancyRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var pregnanciesDto = pregnancies.Select(x => new ClassificationOfPregnancyGetDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();

        return new SuccessListResult<List<ClassificationOfPregnancyGetDto>>(pregnanciesDto);
    }


    public async Task<IListResult<List<MartindaleGetDto>>> GetAllMartindale(CancellationToken cancellationToken)
    {
        var martindale = await _martindaleRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var martindaleDto = martindale.Select(x => new MartindaleGetDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();

        return new SuccessListResult<List<MartindaleGetDto>>(martindaleDto);
    }

    public async Task<IListResult<List<MethodOfUseGetDto>>> GetAllMethodOfUse(CancellationToken cancellationToken)
    {
        var methodOfUse = await _methodOfUseRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var methodOfUseDto = methodOfUse.Select(x => new MethodOfUseGetDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();

        return new SuccessListResult<List<MethodOfUseGetDto>>(methodOfUseDto);
    }

    public async Task<IListResult<List<PharmaceuticalFormsGetDto>>> GetAllPharmaceuticalForms(
        CancellationToken cancellationToken)
    {
        var pharmaceuticalForms = await _pharmaceuticalFormsRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var pharmaceuticalFormsDto = pharmaceuticalForms.Select(x => new PharmaceuticalFormsGetDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();

        return new SuccessListResult<List<PharmaceuticalFormsGetDto>>(pharmaceuticalFormsDto);
    }

    public async Task<IListResult<List<PharmaceuticalFormsGetDto>>> GetAllPharmacologicalClassification(
        CancellationToken cancellationToken)
    {
        var pharmacologicalClassification =
            await _pharmacologicalClassificationRepository
                .GetListAsync
                (
                    x =>
                        x.IsDeleted == false,
                    cancellationToken
                );

        var pharmacologicalClassificationDto =
            pharmacologicalClassification.Select(x => new PharmaceuticalFormsGetDto
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();

        return new SuccessListResult<List<PharmaceuticalFormsGetDto>>(pharmacologicalClassificationDto);
    }


    public async Task<IListResult<List<TherapeuticClassificationGetDto>>> GetAllTherapeuticClassification(
        CancellationToken cancellationToken)
    {
        var therapeuticClassification = await _therapeuticClassificationRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var therapeuticClassificationDto =
            therapeuticClassification.Select(x => new TherapeuticClassificationGetDto
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();

        return new SuccessListResult<List<TherapeuticClassificationGetDto>>(therapeuticClassificationDto);
    }

    public async Task<IDataResult<DrugDetailDto>> GetDrugDetail(int drugId, CancellationToken cancellationToken)
    {
        var persianTitles = GetItemPersianTitles();

        var drugDetail = await
        (
            from drug in _drugRepository.TableNoTracking
            join drugCategory in _drugCategoryRepository.TableNoTracking on drug.DrugCategoryId equals drugCategory.Id
            join drugInformation in _drugInformationRepository.TableNoTracking on drug.Id equals drugInformation.DrugId
            join pharmacologicalClassification in _pharmacologicalClassificationRepository.TableNoTracking on
                drugInformation.PharmacologicalClassificationId equals pharmacologicalClassification.Id
            join therapeuticClassification in _therapeuticClassificationRepository.TableNoTracking on drugInformation
                .TherapeuticClassificationId equals therapeuticClassification.Id
            join martindaleClassification in _martindaleRepository.TableNoTracking on drugInformation
                .MartindaleClassificationId equals martindaleClassification.Id
            join pregnancyClassification in _classificationOfPregnancyRepository.TableNoTracking on drugInformation
                .PregnancyClassificationId equals pregnancyClassification.Id
            join methodOfUse in _methodOfUseRepository.TableNoTracking on drugInformation.MethodOfUseId equals
                methodOfUse.Id
            where drug.Id == drugId &&
                  drug.IsDeleted == false &&
                  drugCategory.IsDeleted == false &&
                  drugInformation.IsDeleted == false &&
                  pharmacologicalClassification.IsDeleted == false &&
                  therapeuticClassification.IsDeleted == false &&
                  martindaleClassification.IsDeleted == false &&
                  pregnancyClassification.IsDeleted == false &&
                  methodOfUse.IsDeleted == false
            select new DrugDetailDto
            {
                FaTitle = drug.FaTitle,
                EnTitle = drug.EnTitle,
                Brand = drug.FaTitle,
                CategoryEnTitle = drugCategory.EnTitle,
                CategoryFaTitle = drugCategory.FaTitle,
                PharmacologicalClassification = new KeyValueDto
                    { Key = "", Value = pharmacologicalClassification.Title },
                TherapeuticClassification = new KeyValueDto { Key = "", Value = therapeuticClassification.Title },
                MartindaleClassification = new KeyValueDto { Key = "", Value = martindaleClassification.Title },
                PregnancyClassification = new KeyValueDto { Key = "", Value = pregnancyClassification.Title },
                MethodOfUse = new KeyValueDto { Key = "", Value = methodOfUse.Title },
                Indication = new KeyValueDto { Key = "", Value = drugInformation.Indication },
                sideEffects = new KeyValueDto { Key = "", Value = drugInformation.SideEffect },
                MechanismOfEffect = new KeyValueDto { Key = "", Value = drugInformation.MechanismOfEffect },
                BeginningOfEffect = new KeyValueDto { Key = "", Value = drugInformation.BeginningOfEffect },
                PeakOfEffect = new KeyValueDto { Key = "", Value = drugInformation.PeakOfEffect },
                Pharmacokinetics = new KeyValueDto { Key = "", Value = drugInformation.Pharmacodynamics },
                PrescriptionIndications = new KeyValueDto { Key = "", Value = drugInformation.PrescriptionIndications },
                Contraindicated = new KeyValueDto { Key = "", Value = drugInformation.Contraindicated },
                IncreaseEffect = new KeyValueDto { Key = "", Value = drugInformation.IncreaseEffect },
                ReduceEffect = new KeyValueDto { Key = "", Value = drugInformation.ReduceEffect },
                AvoidInteractions = new KeyValueDto { Key = "", Value = drugInformation.AvoidInteractions },
                FoodInteractions = new KeyValueDto { Key = "", Value = drugInformation.FoodInteractions },
                DrugInteractions = new KeyValueDto { Key = "", Value = drugInformation.DrugInteractions },
                PoisoningAndTreatment = new KeyValueDto { Key = "", Value = drugInformation.PoisoningAndTreatment },
                Precautions = new KeyValueDto { Key = "", Value = drugInformation.Precautions },
                SpecialAttention = new KeyValueDto { Key = "", Value = drugInformation.SpecialAttention },
                UseInBreastfeedingAndChildren = new KeyValueDto
                    { Key = "", Value = drugInformation.UseInBreastfeedingAndChildren },
                Education = new KeyValueDto { Key = "", Value = drugInformation.Education },
            }
        ).FirstOrDefaultAsync(cancellationToken);

        if (drugDetail == null)
        {
            return new ErrorDataResult<DrugDetailDto>(Messages.NotFound);
        }

        // Now set the Persian titles using the dictionary
        drugDetail.PharmacologicalClassification.Key = persianTitles["PharmacologicalClassification"];
        drugDetail.TherapeuticClassification.Key = persianTitles["TherapeuticClassification"];
        drugDetail.MartindaleClassification.Key = persianTitles["MartindaleClassification"];
        drugDetail.PregnancyClassification.Key = persianTitles["PregnancyClassification"];
        drugDetail.MethodOfUse.Key = persianTitles["MethodOfUse"];
        drugDetail.Indication.Key = persianTitles["Indication"];
        drugDetail.sideEffects.Key = persianTitles["sideEffects"];
        drugDetail.MechanismOfEffect.Key = persianTitles["MechanismOfEffect"];
        drugDetail.BeginningOfEffect.Key = persianTitles["BeginningOfEffect"];
        drugDetail.PeakOfEffect.Key = persianTitles["PeakOfEffect"];
        drugDetail.Pharmacokinetics.Key = persianTitles["Pharmacokinetics"];
        drugDetail.PrescriptionIndications.Key = persianTitles["PrescriptionIndications"];
        drugDetail.Contraindicated.Key = persianTitles["Contraindicated"];
        drugDetail.IncreaseEffect.Key = persianTitles["IncreaseEffect"];
        drugDetail.ReduceEffect.Key = persianTitles["ReduceEffect"];
        drugDetail.AvoidInteractions.Key = persianTitles["AvoidInteractions"];
        drugDetail.FoodInteractions.Key = persianTitles["FoodInteractions"];
        drugDetail.DrugInteractions.Key = persianTitles["DrugInteractions"];
        drugDetail.PoisoningAndTreatment.Key = persianTitles["PoisoningAndTreatment"];
        drugDetail.Precautions.Key = persianTitles["Precautions"];
        drugDetail.SpecialAttention.Key = persianTitles["SpecialAttention"];
        drugDetail.UseInBreastfeedingAndChildren.Key = persianTitles["UseInBreastfeedingAndChildren"];
        drugDetail.Education.Key = persianTitles["Education"];

        var forms = await
        (
            from drugForm in _drugFormRepository.TableNoTracking
            join pharmaceuticalForms in _pharmaceuticalFormsRepository.TableNoTracking on drugForm.PharmaceuticalFormId
                equals pharmaceuticalForms.Id
            where drugForm.DrugId == drugId &&
                  drugForm.IsDeleted == false &&
                  pharmaceuticalForms.IsDeleted == false
            select new PharmaceuticalFormsGetDto
            {
                Id = pharmaceuticalForms.Id,
                Title = pharmaceuticalForms.Title
            }
        ).ToListAsync(cancellationToken);

        drugDetail.forms = forms;

        return new SuccessDataResult<DrugDetailDto>(drugDetail);
    }


    public class PersianComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var cultureInfo = new CultureInfo("fa-IR");
            return string.Compare(x, y, cultureInfo, CompareOptions.None);
        }
    }


    private static Dictionary<string, string> GetItemPersianTitles()
    {
        return new Dictionary<string, string>
        {
            { "PharmacologicalClassification", "طبقه‌بندی داروشناسی" },
            { "TherapeuticClassification", "طبقه‌بندی درمانی" },
            { "MartindaleClassification", "طبقه‌بندی مارتین‌دیل" },
            { "PregnancyClassification", "طبقه‌بندی در دوران بارداری" },
            { "MethodOfUse", "روش مصرف" },
            { "Indication", "موارد مصرف" },
            { "sideEffects", "عوارض جانبی" },
            { "MechanismOfEffect", "مکانیزم اثر" },
            { "BeginningOfEffect", "شروع اثر" },
            { "PeakOfEffect", "اوج اثر" },
            { "Pharmacokinetics", "فارماکوکینتیک" },
            { "PrescriptionIndications", "موارد تجویز" },
            { "Contraindicated", "موارد منع مصرف" },
            { "IncreaseEffect", "افزایش اثر" },
            { "ReduceEffect", "کاهش اثر" },
            { "AvoidInteractions", "اجتناب از تداخل‌ها" },
            { "FoodInteractions", "تداخل‌های غذایی" },
            { "DrugInteractions", "تداخل‌های دارویی" },
            { "PoisoningAndTreatment", "مسمومیت و درمان" },
            { "Precautions", "احتیاطات" },
            { "SpecialAttention", "توجه ویژه" },
            { "UseInBreastfeedingAndChildren", "مصرف در دوران شیردهی و کودکان" },
            { "Education", "آموزش" }
        };
    }
}