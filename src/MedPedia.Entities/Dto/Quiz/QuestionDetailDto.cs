using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Enums;

namespace MedPedia.Entities.Dto.Quiz;

public class QuestionDetailDto : IDto
{
    public int QuizId { get; set; }
    
    public int QuestionId { get; set; }
    
    public int AnswerId { get; set; }
    
    public Status Status { get; set; }
    
    public int UserId { get; set; }
}