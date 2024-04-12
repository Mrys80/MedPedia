using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class ExperimentCategory:BaseEntity
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
}