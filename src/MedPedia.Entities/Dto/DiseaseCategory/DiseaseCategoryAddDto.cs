using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.DiseaseCategory;

public class DiseaseCategoryAddDto:IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
}