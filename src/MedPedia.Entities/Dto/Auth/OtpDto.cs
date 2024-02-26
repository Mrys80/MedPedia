using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Auth;

public class OtpDto:IDto
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
}