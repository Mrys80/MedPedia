using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Quiz:BaseEntity
{
    public int QuizCategoryId { get; set; }
    
    public int QuizTypeId { get; set; }
    public string Title { get; set; }
}