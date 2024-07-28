using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class City : BaseEntity
{
    public int Id { get; set; }

    public string CityName { get; set; }

    public int ProvinceId { get; set; }
}