using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Provinces;

namespace MedPedia.Services.Abstracts;

public interface IProvinceService
{
    Task<ListResult<List<ProvinceDto>>> GetAllProvinces(CancellationToken cancellationToken);
}