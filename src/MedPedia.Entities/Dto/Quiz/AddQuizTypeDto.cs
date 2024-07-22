using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class AddQuizTypeDto : IDto
{
    public string Title { get; set; }
}