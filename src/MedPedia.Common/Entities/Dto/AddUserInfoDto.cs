using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Common.Entities.Dto;

public class AddUserInfoDto:IDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int? CityId { get; set; }
    public int? ProvinceId { get; set; }
    public string? BirthDate { get; set; }
}