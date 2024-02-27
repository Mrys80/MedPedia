using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Drug;

public class DrugAddDto:IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
    public int DrugCategoryId { get; set; }
}