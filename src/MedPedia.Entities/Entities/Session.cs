using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Session : BaseEntity<Guid>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}