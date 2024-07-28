using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Cities;

public class CityDto : IDto
{
    public int Id { get; set; }
    
    public string CityName { get; set; }
}