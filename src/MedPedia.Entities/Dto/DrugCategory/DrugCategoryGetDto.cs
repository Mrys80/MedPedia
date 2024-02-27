using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.DrugCategory;

public class DrugCategoryGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImageId { get; set; }
}