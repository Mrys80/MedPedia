using MedPedia.Common.Entities.Entities;
using MedPedia.Entities.Enums;

namespace MedPedia.Entities.Entities;

public class QuestionDetail : BaseEntity
{
    public int QuizId { get; set; }
    
    public int QuestionId { get; set; }
    
    public int AnswerId { get; set; }
    
    public Status Status { get; set; }
    
    public int UserId { get; set; }
}