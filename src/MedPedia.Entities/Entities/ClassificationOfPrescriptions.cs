using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class ClassificationOfPrescriptions:BaseEntity
{
    public string Title { get; set; }
    public string? ImageId { get; set; } 
}