using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Prescription:BaseEntity
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
    public int ClassificationId { get; set; }
}