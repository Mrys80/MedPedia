using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class UserQuizAddDto : IDto
{
    public int QuizId { get; set; }
    
    public List<QuestionDetailDto> QuestionDetails { get; set; }
}