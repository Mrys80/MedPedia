using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.PrescriptionWriting;

public class PrescriptionWritingCategoryAddDto : IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
}