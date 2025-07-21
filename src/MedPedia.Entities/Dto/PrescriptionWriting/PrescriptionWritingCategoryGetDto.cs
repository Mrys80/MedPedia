using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.PrescriptionWriting;

public class PrescriptionWritingCategoryGetDto : IDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string? ImageId { get; set; }
}