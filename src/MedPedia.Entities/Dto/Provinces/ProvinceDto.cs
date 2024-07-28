using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Provinces;

public class ProvinceDto : IDto
{
    public int Id { get; set; }
    
    public string ProvinceName { get; set; }
}