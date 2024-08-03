using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Cities;
using MedPedia.Entities.Dto.Provinces;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Teb.Data.Abstracts;

namespace MedPedia.Services.Concretes;

public class ProvinceService (IRepository<Province> provinceRepository , IRepository<City> cityRepository) : IProvinceService , ICityService , IScopedDependency
{
    public async Task<ListResult<List<ProvinceDto>>> GetAllProvinces(CancellationToken cancellationToken)
    {
        var province = await provinceRepository.GetListAsync(
            p =>
                p.IsDeleted == false,cancellationToken: cancellationToken);

        var provinceDtos = province.Adapt<List<ProvinceDto>>();

        return new SuccessListResult<List<ProvinceDto>>(provinceDtos);
    }

    public async Task<ListResult<List<CityDto>>> GetAllCities(int provinceId, CancellationToken cancellationToken)
    {
        var cities = await cityRepository.GetListAsync(
            c => c.IsDeleted == false && c.ProvinceId == provinceId,
            cancellationToken: cancellationToken
        );

        var cityDtos = cities.Adapt<List<CityDto>>();

        return new SuccessListResult<List<CityDto>>(cityDtos);
    }
}