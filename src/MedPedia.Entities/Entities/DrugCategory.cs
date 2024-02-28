using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class DrugCategory : BaseEntity
{
    public string EnTitle { get; set; }
    public string FaTitle { get; set; }
    public string? ImageId { get; set; }
}