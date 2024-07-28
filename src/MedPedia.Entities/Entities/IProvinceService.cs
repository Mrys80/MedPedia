using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Provinces;

namespace MedPedia.Entities.Entities;

public interface IProvinceService
{
    Task<ListResult<List<ProvinceDto>>> GetAllProvinces(CancellationToken cancellationToken);
}