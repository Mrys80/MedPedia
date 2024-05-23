using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class QuizCategoryDto : IDto
{
    public int Id { get; set; }

    public string Title { get; set; }
}