using System.Globalization;
using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Experiment;
using MedPedia.Entities.Dto.Prescription;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
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


    public class PersianComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var cultureInfo = new CultureInfo("fa-IR");
            return string.Compare(x, y, cultureInfo, CompareOptions.None);
        }
    }
}