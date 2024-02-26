using MedPedia.Common.Entities.Dto;

namespace MedPedia.Entities.Dto.Session;

public class SessionDto : BaseDto<Guid>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DateTime RegDate { get; set; }
    public int RegUserId { get; set; }
}