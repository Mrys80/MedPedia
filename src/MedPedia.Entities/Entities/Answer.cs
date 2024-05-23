using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Answer : BaseEntity
{
    public int Id { get; set; }
    
    public int QuestionId { get; set; }
    
    public bool IsCorrect { get; set; }
    
    public string Title { get; set; }
}