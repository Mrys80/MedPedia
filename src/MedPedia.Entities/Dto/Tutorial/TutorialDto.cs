using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Tutorial;

public class TutorialDto : IDto
{
    public int Id { get; set; }
    
    public string Image { get; set; }
    
    public string Title { get; set; }
}