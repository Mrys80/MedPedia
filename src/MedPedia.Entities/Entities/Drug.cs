using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Drug : BaseEntity
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
    public int DrugCategoryId { get; set; }
}