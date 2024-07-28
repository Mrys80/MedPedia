using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Common.Entities.Dto;

public class UserDto:IDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? BirthDate { get; set; }
    public string PhoneNumber { get; set; }
}