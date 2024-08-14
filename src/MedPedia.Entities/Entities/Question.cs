using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Question : BaseEntity
{
    public string Title { get; set; }
    
    public int QuizId { get; set; }
}