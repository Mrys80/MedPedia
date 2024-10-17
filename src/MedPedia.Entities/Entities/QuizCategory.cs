using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class QuizCategory : BaseEntity
{
    public int LessonId { get; set; }
    public string Title { get; set; }
}