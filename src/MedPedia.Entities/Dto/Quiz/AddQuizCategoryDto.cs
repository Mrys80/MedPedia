using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class AddQuizCategoryDto:IDto
{
    public int QuizTypeId { get; set; }
    public string Title { get; set; }
}