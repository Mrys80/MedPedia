using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class UserQuizHistoryDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }

    public string QuizCategoryTitle { get; set; }
    
    public string LessonTitle { get; set; }
    
    public string QuizTitle { get; set; }
    
    public int CorrectCount { get; set; }
    
    public int MistakeCount { get; set; }
    public int QuestionCount { get; set; }
    
    public DateTime RegDateTime { get; set; }
}