using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Quiz:BaseEntity
{
    public int Id { get; set; }
    public int QuizCategoryId { get; set; }
    public string Title { get; set; }
}