using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class QuizTypeDto : IDto
{
    public int QuizTypeId { get; set; }
    
    public int QuizCategoryId { get; set; }
}