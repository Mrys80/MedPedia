using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Prescription;

public class PrescriptionGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImageId { get; set; }
}