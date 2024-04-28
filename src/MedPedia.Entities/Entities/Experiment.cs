using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Experiment:BaseEntity
{
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
   
    public string? ImageId { get; set; }
    public int ExperimentCategoryId { get; set; }
}