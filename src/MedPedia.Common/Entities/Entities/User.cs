namespace MedPedia.Common.Entities.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? CityId { get; set; }

    public int? ProvinceId { get; set; }

    public string? BirthDate { get; set; }

    public int? ActivityTypeId { get; set; }

    public string PhoneNumber { get; set; }
}