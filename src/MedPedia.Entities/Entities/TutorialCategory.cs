using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class TutorialCategory : BaseEntity
{
    public string Title { get; set; }
    
    public string? Image { get; set; }
}