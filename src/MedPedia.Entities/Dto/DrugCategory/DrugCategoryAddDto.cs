using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.DrugCategory;

public class DrugCategoryAddDto:IDto
{
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string? ImageId { get; set; }
}