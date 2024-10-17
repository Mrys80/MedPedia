using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class AddQuizDto:IDto
{
    public int QuizCategoryId { get; set; }
    public int QuizType { get; set; }
    public string Title { get; set; }
}