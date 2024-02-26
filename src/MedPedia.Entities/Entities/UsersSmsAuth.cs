using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class UsersSmsAuth:BaseEntity
{
    public int UserId { get; set; }
    public string SmsCode { get; set; }
}