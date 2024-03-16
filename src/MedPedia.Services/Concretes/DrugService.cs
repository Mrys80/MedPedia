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
    private readonly IRepository<Indications> _indicationsRepository;
    private readonly IRepository<MartindaleClassification> _martindaleRepository;
    private readonly IRepository<MethodOfUse> _methodOfUseRepository;
    private readonly IRepository<PharmaceuticalForms> _pharmaceuticalFormsRepository;
    private readonly IRepository<PharmacologicalClassification> _pharmacologicalClassificationRepository;
    private readonly IRepository<SideEffect> _sideEffectRepository;
    private readonly IRepository<TherapeuticClassification> _therapeuticClassificationRepository;
    private readonly IRepository<DrugInformation> _drugInformationRepository;
    private readonly IRepository<DrugForms> _drugFormRepository;
    private readonly IRepository<DrugSideEffect> _drugSideEffectRepository;


    public DrugService(IRepository<Drug> drugRepository, IRepository<DrugCategory> drugCategoryRepository,
        IRepository<ClassificationOfPregnancy> classificationOfPregnancyRepository,
        IRepository<MartindaleClassification> martindaleRepository, IRepository<MethodOfUse> methodOfUseRepository,
        IRepository<Indications> indicationsRepository, IRepository<PharmaceuticalForms> pharmaceuticalFormsRepository,
        IRepository<PharmacologicalClassification> pharmacologicalClassificationRepository,
        IRepository<SideEffect> sideEffectRepository,
        IRepository<TherapeuticClassification> therapeuticClassificationRepository,
        IRepository<DrugInformation> drugInformationRepository, IRepository<DrugForms> drugFormRepository,
        IRepository<DrugSideEffect> drugSideEffectRepository)
    {
        _drugRepository = drugRepository;
        _drugCategoryRepository = drugCategoryRepository;
        _classificationOfPregnancyRepository = classificationOfPregnancyRepository;
        _martindaleRepository = martindaleRepository;
        _methodOfUseRepository = methodOfUseRepository;
        _indicationsRepository = indicationsRepository;
        _pharmaceuticalFormsRepository = pharmaceuticalFormsRepository;
        _pharmacologicalClassificationRepository = pharmacologicalClassificationRepository;
        _sideEffectRepository = sideEffectRepository;
        _therapeuticClassificationRepository = therapeuticClassificationRepository;
        _drugInformationRepository = drugInformationRepository;
        _drugFormRepository = drugFormRepository;
        _drugSideEffectRepository = drugSideEffectRepository;
    }

    public async Task<IListResult<List<DrugGetDto>>> GetAllDrug(
        CancellationToken cancellationToken)
    {
        var drugs = await _drugRepository
            .GetListAsync(drug => drug.IsDeleted == false, cancellationToken);
        var drugsDto = drugs.Select(drug => new DrugGetDto
            {
                Id = drug.Id,
                FaTitle = drug.FaTitle,
                EnTitle = drug.EnTitle,
                Brand = drug.Brand,
                ImageId = drug.ImageId
            })
            .OrderBy(drugDto => drugDto.FaTitle, new PersianComparer())
            .ToList();
        return new SuccessListResult<List<DrugGetDto>>(drugsDto);
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

    public async Task<IListResult<List<IndicationsGetDto>>> GetAllIndications(CancellationToken cancellationToken)
    {
        var indications = await _indicationsRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var indicationsDto = indications.Select(x => new IndicationsGetDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();

        return new SuccessListResult<List<IndicationsGetDto>>(indicationsDto);
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

    public async Task<IListResult<List<SideEffectGetDto>>> GetAllSideEffect(CancellationToken cancellationToken)
    {
        var sideEffects = await _sideEffectRepository
            .GetListAsync
            (
                x =>
                    x.IsDeleted == false,
                cancellationToken
            );

        var sideEffectsDto = sideEffects.Select(x => new SideEffectGetDto
        {
            Id = x.Id,
            Title = x.Title
        }).ToList();

        return new SuccessListResult<List<SideEffectGetDto>>(sideEffectsDto);
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
            join indication in _indicationsRepository.TableNoTracking on drugInformation.IndicationId equals indication
                .Id
            where
                drug.Id == drugId &&
                drug.IsDeleted == false &&
                drugCategory.IsDeleted == false &&
                drugInformation.IsDeleted == false &&
                pharmacologicalClassification.IsDeleted == false &&
                therapeuticClassification.IsDeleted == false &&
                martindaleClassification.IsDeleted == false &&
                pregnancyClassification.IsDeleted == false &&
                methodOfUse.IsDeleted == false &&
                indication.IsDeleted == false
            select new DrugDetailDto
            {
                FaTitle = drug.FaTitle,
                EnTitle = drug.EnTitle,
                Brand = drug.FaTitle,
                CategoryEnTitle = drugCategory.EnTitle,
                CategoryFaTitle = drugCategory.FaTitle,
                PharmacologicalClassification = pharmacologicalClassification.Title,
                TherapeuticClassification = therapeuticClassification.Title,
                MartindaleClassification = martindaleClassification.Title,
                PregnancyClassification = pregnancyClassification.Title,
                MethodOfUse = methodOfUse.Title,
                Indication = indication.Title,
                MechanismOfEffect = drugInformation.MechanismOfEffect,
                BeginningOfEffect = drugInformation.BeginningOfEffect,
                PeakOfEffect = drugInformation.PeakOfEffect,
                Pharmacokinetics = drugInformation.Pharmacokinetics,
                PrescriptionIndications = drugInformation.PrescriptionIndications,
                Contraindicated = drugInformation.Contraindicated,
                IncreaseEffect = drugInformation.IncreaseEffect,
                ReduceEffect = drugInformation.ReduceEffect,
                AvoidInteractions = drugInformation.AvoidInteractions,
                FoodInteractions = drugInformation.FoodInteractions,
                DrugInteractions = drugInformation.DrugInteractions,
                PoisoningAndTreatment = drugInformation.PoisoningAndTreatment,
                Precautions = drugInformation.Precautions,
                SpecialAttention = drugInformation.SpecialAttention,
                UseInBreastfeedingAndChildren = drugInformation.UseInBreastfeedingAndChildren,
                Education = drugInformation.Education,
            }
        ).FirstOrDefaultAsync(cancellationToken);


        if (drugDetail == null)
        {
            return new ErrorDataResult<DrugDetailDto>(Messages.NotFound);
        }


        var forms =
            await
            (
                from drugForm in _drugFormRepository.TableNoTracking
                join pharmaceuticalForms in _pharmaceuticalFormsRepository.TableNoTracking on drugForm
                    .PharmaceuticalFormId equals pharmaceuticalForms.Id
                where
                    drugForm.DrugId == drugId &&
                    drugForm.IsDeleted == false &&
                    pharmaceuticalForms.IsDeleted == false
                select new PharmaceuticalFormsGetDto
                {
                    Id = pharmaceuticalForms.Id,
                    Title = pharmaceuticalForms.Title
                }
            ).ToListAsync(cancellationToken);


        var sideEffects =
            await
            (
                from drugSideEffect in _drugSideEffectRepository.TableNoTracking
                join sideEffect in _sideEffectRepository.TableNoTracking on drugSideEffect
                    .SideEffectId equals sideEffect.Id
                where
                    drugSideEffect.DrugId == drugId &&
                    drugSideEffect.IsDeleted == false &&
                    sideEffect.IsDeleted == false
                select new SideEffectGetDto
                {
                    Id = sideEffect.Id,
                    Title = sideEffect.Title,
                    Description = sideEffect.Description,
                }
            ).ToListAsync(cancellationToken);


        drugDetail.sideEffects = sideEffects;
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
    
}