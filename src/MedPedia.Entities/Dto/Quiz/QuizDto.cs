using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class QuizDto : IDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public bool IsDone { get; set; }
}