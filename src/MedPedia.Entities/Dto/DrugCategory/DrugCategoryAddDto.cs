using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.DrugCategory;

public class DrugCategoryAddDto:IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
}