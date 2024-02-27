using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Prescription;

public class PrescriptionAddDto:IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
    public int ClassificationId { get; set; }
}