using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Disease;

public class DiseaseAddDto:IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
    public int DiseaseCategoryId { get; set; }
}