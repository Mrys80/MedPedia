using System.Globalization;
using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.Experiment;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class ExperimentService : IExperimentService, IScopedDependency
{
    private readonly IRepository<Experiment> _experimentRepository;
    private readonly IRepository<ExperimentCategory> _experimentCategoryRepository;
    private readonly IRepository<ExperimentInformation> _experimentInformationRepository;

    public ExperimentService(IRepository<ExperimentCategory> experimentCategoryRepository,
        IRepository<Experiment> experimentRepository,
        IRepository<ExperimentInformation> experimentInformationRepository)
    {
        _experimentCategoryRepository = experimentCategoryRepository;
        _experimentRepository = experimentRepository;
        _experimentInformationRepository = experimentInformationRepository;
    }


    public async Task<IListResult<List<ExperimentGetDto>>> GetAllExperiment(CancellationToken cancellationToken)
    {
        var experiments = await _experimentRepository
            .GetListAsync(ex => ex.IsDeleted == false, cancellationToken);

        var sortedExperiment = experiments
            .Select(ex => new ExperimentGetDto
            {
                Id = ex.Id,
                FaTitle = ex.FaTitle,
                EnTitle = ex.EnTitle,
                // Abbreviation = ex.Abbreviation,
                // Usage = ex.Usage,
                // InterferingFactors = ex.InterferingFactors,
                // Description = ex.Description,
                // NormalSuffering = ex.NormalSuffering,
                // ScopeOfCrisis = ex.ScopeOfCrisis,
                // CommonCausesOfDeficiencyOrExcess = ex.CommonCausesOfDeficiencyOrExcess,
                // RelatedSymptomsOrDisease = ex.RelatedSymptomsOrDisease,
                // PreSamplingConditions = ex.PreSamplingConditions,
                // RequiredSample = ex.RequiredSample,
                // VialType = ex.VialType,
                // RelatedTests = ex.RelatedTests,
                // ClinicaNotes = ex.ClinicaNotes,
                // TestLimitations = ex.TestLimitations,
                ImageId = ex.ImageId
            })
            .OrderBy(drugDto => drugDto.FaTitle, new PersianComparer())
            .ToList();


        return new SuccessListResult<List<ExperimentGetDto>>(sortedExperiment);
    }

    public async Task<IListResult<List<ExperimentGetDto>>> GetAllExperimentByCategoryId(int categoryId,
        CancellationToken cancellationToken)
    {
        var experiments = await _experimentRepository
            .GetListAsync(
                ex =>
                    ex.IsDeleted == false &&
                    ex.ExperimentCategoryId == categoryId
                , cancellationToken);

        var experimentsDto = experiments.Select(ex => new ExperimentGetDto
            {
                Id = ex.Id,
                FaTitle = ex.FaTitle,
                EnTitle = ex.EnTitle,
                // Abbreviation = ex.Abbreviation,
                // Usage = ex.Usage,
                // InterferingFactors = ex.InterferingFactors,
                // Description = ex.Description,
                // NormalSuffering = ex.NormalSuffering,
                // ScopeOfCrisis = ex.ScopeOfCrisis,
                // CommonCausesOfDeficiencyOrExcess = ex.CommonCausesOfDeficiencyOrExcess,
                // RelatedSymptomsOrDisease = ex.RelatedSymptomsOrDisease,
                // PreSamplingConditions = ex.PreSamplingConditions,
                // RequiredSample = ex.RequiredSample,
                // VialType = ex.VialType,
                // RelatedTests = ex.RelatedTests,
                // ClinicaNotes = ex.ClinicaNotes,
                // TestLimitations = ex.TestLimitations,
                ImageId = ex.ImageId
            })
            .ToList();

        return new SuccessListResult<List<ExperimentGetDto>>(experimentsDto);
    }

    public async Task<IDataResult<ExperimentGetDto>> AddExperiment(CurrentUserDto currentUser,
        ExperimentAddDto experimentAddDto, CancellationToken cancellationToken)
    {
        var ex = new Experiment
        {
            FaTitle = experimentAddDto.FaTitle,
            EnTitle = experimentAddDto.EnTitle,
            // Abbreviation = experimentAddDto.Abbreviation,
            // Usage = experimentAddDto.Usage,
            // InterferingFactors = experimentAddDto.InterferingFactors,
            // Description = experimentAddDto.Description,
            // NormalSuffering = experimentAddDto.NormalSuffering,
            // ScopeOfCrisis = experimentAddDto.ScopeOfCrisis,
            // CommonCausesOfDeficiencyOrExcess = experimentAddDto.CommonCausesOfDeficiencyOrExcess,
            // RelatedSymptomsOrDisease = experimentAddDto.RelatedSymptomsOrDisease,
            // PreSamplingConditions = experimentAddDto.PreSamplingConditions,
            // RequiredSample = experimentAddDto.RequiredSample,
            // VialType = experimentAddDto.VialType,
            // RelatedTests = experimentAddDto.RelatedTests,
            // ClinicaNotes = experimentAddDto.ClinicaNotes,
            // TestLimitations = experimentAddDto.TestLimitations,
            ImageId = experimentAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _experimentRepository.AddAsync(ex, cancellationToken);

        var exDto = ex.Adapt<ExperimentGetDto>();
        return new SuccessDataResult<ExperimentGetDto>(exDto);
    }

    public async Task<IListResult<List<ExperimentCategoryGetDto>>> GetAllExperimentCategory(
        CancellationToken cancellationToken)
    {
        var experimentCategories = await _experimentCategoryRepository
            .GetListAsync(exc => exc.IsDeleted == false, cancellationToken);

        var experimentCategoriesDto = experimentCategories.Select(exc => new ExperimentCategoryGetDto
                { Id = exc.Id, Title = exc.Title, ImageId = exc.ImageId })
            .ToList();
        return new SuccessListResult<List<ExperimentCategoryGetDto>>(experimentCategoriesDto);
    }


    public async Task<IDataResult<ExperimentCategoryGetDto>> AddExperimentCategory(CurrentUserDto currentUser,
        ExperimentCategoryAddDto experimentCategoryAddDto,
        CancellationToken cancellationToken)
    {
        var experimentCategory = new ExperimentCategory
        {
            Title = experimentCategoryAddDto.Title,
            ImageId = experimentCategoryAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _experimentCategoryRepository.AddAsync(experimentCategory, cancellationToken);

        var experimentCategoryDto = experimentCategory.Adapt<ExperimentCategoryGetDto>();
        return new SuccessDataResult<ExperimentCategoryGetDto>(experimentCategoryDto);
    }

    public async Task<IDataResult<ExperimentDetailDto>> GetExperimentInfo(int experimentId,
        CancellationToken cancellationToken)
    {
        var experimentInfo = await _experimentInformationRepository
            .GetAsync(
                x => x.Id == experimentId,
                cancellationToken
            );

        var experimentInfoDto = experimentInfo.Adapt<ExperimentDetailDto>();
        return new SuccessDataResult<ExperimentDetailDto>(experimentInfoDto);
    }

    public async Task<IDataResult<ExperimentInformationDto>> GetExperimentDetail(
        int experimentId,
        CancellationToken cancellationToken)
    {
        var persianTitles = GetItemPersianTitles();

        var rawQuery =
            from experiment in _experimentRepository.TableNoTracking.Where(e => e.Id == experimentId && !e.IsDeleted)
            join experimentCategory in _experimentCategoryRepository.TableNoTracking.Where(ec => !ec.IsDeleted)
                on experiment.ExperimentCategoryId equals experimentCategory.Id
            join experimentInformation in _experimentInformationRepository.TableNoTracking.Where(ei => !ei.IsDeleted)
                on experiment.Id equals experimentInformation.ExperimentId
            select new
            {
                Experiment = experiment,
                ExperimentCategory = experimentCategory,
                ExperimentInformation = experimentInformation
            };

        var rawData = await rawQuery.FirstOrDefaultAsync(cancellationToken);

        if (rawData == null)
        {
            return new ErrorDataResult<ExperimentInformationDto>(Messages.NotFound);
        }

        var experimentDetail = new ExperimentInformationDto
        {
            ExperimentId = rawData.Experiment.Id,
            FaTitle = rawData.Experiment.FaTitle,
            EnTitle = rawData.Experiment.EnTitle,
            Abbreviation = new KeyValueDto
            {
                Key = persianTitles["Abbreviation"],
                Value = rawData.ExperimentInformation.Abbreviation
            },
            Title = new KeyValueDto
            {
                Key = persianTitles["Title"],
                Value = rawData.ExperimentCategory.Title
            },
            ImageId = rawData.ExperimentCategory.ImageId == null
                ? null
                : new KeyValueDto
                {
                    Key = persianTitles["ImageId"],
                    Value = rawData.ExperimentCategory.ImageId
                },
            Usage = new KeyValueDto
            {
                Key = persianTitles["Usage"],
                Value = rawData.ExperimentInformation.Usage
            },
            InterferingFactors = new KeyValueDto
            {
                Key = persianTitles["InterferingFactors"],
                Value = rawData.ExperimentInformation.InterferingFactors
            },
            Description = new KeyValueDto
            {
                Key = persianTitles["Description"],
                Value = rawData.ExperimentInformation.Description
            },
            NormalSuffering = new KeyValueDto
            {
                Key = persianTitles["NormalSuffering"],
                Value = rawData.ExperimentInformation.NormalSuffering
            },
            ScopeOfCrisis = new KeyValueDto
            {
                Key = persianTitles["ScopeOfCrisis"],
                Value = rawData.ExperimentInformation.ScopeOfCrisis
            },
            CommonCausesOfDeficiencyOrExcess = new KeyValueDto
            {
                Key = persianTitles["CommonCausesOfDeficiencyOrExcess"],
                Value = rawData.ExperimentInformation.CommonCausesOfDeficiencyOrExcess
            },
            RelatedSymptomsOrDisease = new KeyValueDto
            {
                Key = persianTitles["RelatedSymptomsOrDisease"],
                Value = rawData.ExperimentInformation.RelatedSymptomsOrDisease
            },
            PreSamplingConditions = new KeyValueDto
            {
                Key = persianTitles["PreSamplingConditions"],
                Value = rawData.ExperimentInformation.PreSamplingConditions
            },
            RequiredSample = new KeyValueDto
            {
                Key = persianTitles["RequiredSample"],
                Value = rawData.ExperimentInformation.RequiredSample
            },
            VialType = new KeyValueDto
            {
                Key = persianTitles["VialType"],
                Value = rawData.ExperimentInformation.VialType
            },
            RelatedTests = new KeyValueDto
            {
                Key = persianTitles["RelatedTests"],
                Value = rawData.ExperimentInformation.RelatedTests
            },
            ClinicaNotes = new KeyValueDto
            {
                Key = persianTitles["ClinicaNotes"],
                Value = rawData.ExperimentInformation.ClinicaNotes
            },
            TestLimitations = new KeyValueDto
            {
                Key = persianTitles["TestLimitations"],
                Value = rawData.ExperimentInformation.TestLimitations
            }
        };

        return new SuccessDataResult<ExperimentInformationDto>(experimentDetail);
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
            { "Abbreviation", "مخفف" },
            { "Usage", "کاربرد" },
            { "InterferingFactors", "عوامل مداخله‌کننده" },
            { "Description", "توضیحات" },
            { "NormalSuffering", "محدوده نرمال" },
            { "ScopeOfCrisis", "محدوده بحرانی" },
            { "CommonCausesOfDeficiencyOrExcess", "دلایل شایع کمبود یا افزایش" },
            { "RelatedSymptomsOrDisease", "علائم یا بیماری‌های مرتبط" },
            { "PreSamplingConditions", "شرایط قبل از نمونه‌گیری" },
            { "RequiredSample", "نمونه موردنیاز" },
            { "VialType", "نوع ویال" },
            { "RelatedTests", "آزمایش‌های مرتبط" },
            { "ClinicaNotes", "یادداشت‌های کلینیکی" },
            { "TestLimitations", "محدودیت‌های آزمایش" },
            { "Title", "عنوان" },
            { "ImageId", "شناسه تصویر" }
        };
    }
}