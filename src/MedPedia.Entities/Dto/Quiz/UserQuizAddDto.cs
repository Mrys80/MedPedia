using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class UserQuizAddDto : IDto
{
    public int QuizId { get; set; }
    
    public int CorrectCount { get; set; }
    
    public int MistakeCount { get; set; }
}