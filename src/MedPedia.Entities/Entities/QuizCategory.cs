using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class QuizCategory : BaseEntity
{
    public int Id { get; set; }
    
    public int QuizTypeId { get; set; }

    public string Title { get; set; }
}