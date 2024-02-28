using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Drug;

public class DrugAddDto : IDto
{
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string Brand { get; set; }
    public string? ImageId { get; set; }
    public int DrugCategoryId { get; set; }
}