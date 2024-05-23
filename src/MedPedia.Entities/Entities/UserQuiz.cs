using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class UserQuiz : BaseEntity
{
    public int Id { get; set; }
    
    public int QuizId { get; set; }
    
    public int CorrectCount { get; set; }
    
    public int MistakeCount { get; set; }
}