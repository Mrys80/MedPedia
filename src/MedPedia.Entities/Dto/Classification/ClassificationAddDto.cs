using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Classification;

public class ClassificationAddDto:IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
}