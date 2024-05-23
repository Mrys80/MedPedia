using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class QuizType : BaseEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
}