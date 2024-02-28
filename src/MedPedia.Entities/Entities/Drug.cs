using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Drug : BaseEntity
{
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string Brand { get; set; }
    public string? ImageId { get; set; }
    public int DrugCategoryId { get; set; }

   
}