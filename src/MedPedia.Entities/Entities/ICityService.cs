using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Cities;

namespace MedPedia.Entities.Entities;

public interface ICityService
{
    Task<ListResult<List<CityDto>>> GetAllCities(int provinceId,CancellationToken cancellationToken);
}