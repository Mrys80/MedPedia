using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class AddLessonDto : IDto
{
    public string Title { get; set; }
}