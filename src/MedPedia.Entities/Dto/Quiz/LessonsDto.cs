using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class LessonsDto:IDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
}