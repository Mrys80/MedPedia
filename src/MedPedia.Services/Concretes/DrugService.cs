using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Drug;
using MedPedia.Entities.Dto.DrugCategory;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class DrugService : IDrugService, IDrugCategoryService, IScopedDependency
{
    private readonly IRepository<Drug> _drugRepository;
    private readonly IRepository<DrugCategory> _drugCategoryRepository;


    public DrugService(IRepository<Drug> drugRepository, IRepository<DrugCategory> drugCategoryRepository)
    {
        _drugRepository = drugRepository;
        _drugCategoryRepository = drugCategoryRepository;
    }

    public async Task<IListResult<List<DrugGetDto>>> GetAllDrug(
        CancellationToken cancellationToken)
    {
        var drugs = await _drugRepository
            .GetListAsync(drug => drug.IsDeleted == false, cancellationToken);
        var drugsDto = drugs.Select(drug => new DrugGetDto { Id = drug.Id, Title = drug.Title, ImageId = drug.ImageId })
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
        var drugsDto = drugs.Select(drug => new DrugGetDto { Id = drug.Id, Title = drug.Title, ImageId = drug.ImageId })
            .ToList();
        return new SuccessListResult<List<DrugGetDto>>(drugsDto);
    }

    public async Task<IDataResult<DrugGetDto>> AddDrug(CurrentUserDto currentUser, DrugAddDto drugAddDto,
        CancellationToken cancellationToken)
    {
        var drug = new Drug
        {
            Title = drugAddDto.Title,
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

    public async Task<IListResult<List<DrugGetDto>>> SearchDrug(string text,CancellationToken cancellationToken)
    {
        var drugs = await _drugRepository
            .GetListAsync(drug => drug.IsDeleted == false && drug.Title.Contains(text), cancellationToken);
        var drugsDto = drugs.Select(drug => new DrugGetDto { Id = drug.Id, Title = drug.Title, ImageId = drug.ImageId })
            .ToList();
        return new SuccessListResult<List<DrugGetDto>>(drugsDto);
    }


    public async Task<IListResult<List<DrugCategoryGetDto>>> GetAllDrugCategories(CancellationToken cancellationToken)
    {
        var drugsCategory = await _drugCategoryRepository
            .GetListAsync(drugCategory => drugCategory.IsDeleted == false, cancellationToken);
        var drugsCategoryDto = drugsCategory.Select(drug => new DrugCategoryGetDto
                { Id = drug.Id, Title = drug.Title, ImageId = drug.ImageId })
            .ToList();
        return new SuccessListResult<List<DrugCategoryGetDto>>(drugsCategoryDto);
    }

    public async Task<IListResult<List<DrugCategoryGetDto>>> SearchDrugCategories(string text,CancellationToken cancellationToken)
    {
        var drugsCategory = await _drugCategoryRepository
            .GetListAsync(drugCategory => drugCategory.IsDeleted == false && drugCategory.Title.Contains(text), cancellationToken);
        var drugsCategoryDto = drugsCategory.Select(drug => new DrugCategoryGetDto
                { Id = drug.Id, Title = drug.Title, ImageId = drug.ImageId })
            .ToList();
        return new SuccessListResult<List<DrugCategoryGetDto>>(drugsCategoryDto);
    }

    public async Task<IDataResult<DrugCategoryGetDto>> AddDrugCategories(
        CurrentUserDto currentUser,
        DrugCategoryAddDto drugCategoryAddDto, CancellationToken cancellationToken)
    {
        var drugCategory = new DrugCategory
        {
            Title = drugCategoryAddDto.Title,
            ImageId = drugCategoryAddDto.ImageId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _drugCategoryRepository.AddAsync(drugCategory, cancellationToken);

        var drugCategoryDto = drugCategory.Adapt<DrugCategoryGetDto>();
        return new SuccessDataResult<DrugCategoryGetDto>(drugCategoryDto);
    }
}